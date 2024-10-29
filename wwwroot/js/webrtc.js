const connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .build();

let localStream;
let remoteStream;
let peerConnection;

const configuration = {
    iceServers: [
        {
            urls: "stun:stun.l.google.com:19302"
        }
    ]
};

async function startVideoChat() {
    try {
        localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
        document.getElementById("localVideo").srcObject = localStream;

        connection.on("ReceiveSignal", async (signal) => {
            if (signal.type === "offer") {
                await handleOffer(signal);
            } else if (signal.type === "answer") {
                await handleAnswer(signal);
            } else if (signal.type === "ice-candidate") {
                await handleIceCandidate(signal);
            }
        });

        await connection.start();
        console.log("SignalR connected");

        connection.on("ReceiveGameUpdate", (gameState) => {
            // Handle game state update
        });

        connection.on("ReceiveScoreUpdate", (player1Score, player2Score) => {
            // Handle score update
        });

        connection.on("ReceiveMoveValidation", (isValid, message) => {
            // Handle move validation
        });

    } catch (error) {
        console.error("Error starting video chat:", error);
    }
}

async function createOffer() {
    peerConnection = new RTCPeerConnection(configuration);
    localStream.getTracks().forEach(track => peerConnection.addTrack(track, localStream));

    peerConnection.onicecandidate = (event) => {
        if (event.candidate) {
            connection.invoke("SendSignal", { type: "ice-candidate", candidate: event.candidate });
        }
    };

    peerConnection.ontrack = (event) => {
        if (!remoteStream) {
            remoteStream = new MediaStream();
            document.getElementById("remoteVideo").srcObject = remoteStream;
        }
        remoteStream.addTrack(event.track);
    };

    const offer = await peerConnection.createOffer();
    await peerConnection.setLocalDescription(offer);
    connection.invoke("SendSignal", { type: "offer", sdp: offer.sdp });
}

async function handleOffer(signal) {
    peerConnection = new RTCPeerConnection(configuration);
    localStream.getTracks().forEach(track => peerConnection.addTrack(track, localStream));

    peerConnection.onicecandidate = (event) => {
        if (event.candidate) {
            connection.invoke("SendSignal", { type: "ice-candidate", candidate: event.candidate });
        }
    };

    peerConnection.ontrack = (event) => {
        if (!remoteStream) {
            remoteStream = new MediaStream();
            document.getElementById("remoteVideo").srcObject = remoteStream;
        }
        remoteStream.addTrack(event.track);
    };

    await peerConnection.setRemoteDescription(new RTCSessionDescription({ type: "offer", sdp: signal.sdp }));
    const answer = await peerConnection.createAnswer();
    await peerConnection.setLocalDescription(answer);
    connection.invoke("SendSignal", { type: "answer", sdp: answer.sdp });
}

async function handleAnswer(signal) {
    await peerConnection.setRemoteDescription(new RTCSessionDescription({ type: "answer", sdp: signal.sdp }));
}

async function handleIceCandidate(signal) {
    await peerConnection.addIceCandidate(new RTCIceCandidate(signal.candidate));
}

document.getElementById("startVideoChatButton").addEventListener("click", startVideoChat);
document.getElementById("createOfferButton").addEventListener("click", createOffer);
