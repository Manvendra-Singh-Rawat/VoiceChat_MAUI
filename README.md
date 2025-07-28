# 📞 Voice Chat App (.NET MAUI + NAudio + SIPSorcery)

A lightweight real-time voice chat application built using .NET MAUI (Windows only) for the client, a .NET Console UDP relay server, and audio streaming via NAudio and SIPSorcery.

This project demonstrates a simple RTP-based communication system where multiple clients can send and receive voice data in real-time, with no SIP server required.

# 🔧 Tech Stack
.NET MAUI (Windows) – Cross-platform UI framework (used here for Windows desktop)

NAudio – Microphone capture and speaker playback (PCM 16-bit audio)

UDP (RTP-style) – Low-latency transmission using SIPSorcery's RTP classes

SIPSorcery – RTP packet formatting and optional RTCP support

.NET Console Server – Simple relay to forward voice data to all connected peers

# ✨ Features
🎙️ Live mic input (16-bit PCM, 44100 Hz)

🔁 Real-time audio relay via central UDP server

🔈 Incoming voice streamed and played on the fly

🚫 Echo prevention (voice is not played back to sender)

🕒 Client timeout detection (removes inactive clients)

🔧 Easily extendable to support Opus, RTCP, or SIP
