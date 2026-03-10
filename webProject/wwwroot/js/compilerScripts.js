async function sendMessage() {
    const inputField = document.getElementById('chat-input');
    const chatBox = document.getElementById('chat-box');
    const message = inputField.value;

    if (message.trim() === "") return;

    // Display the user's message as a bubble
    const userMsg = document.createElement('div');
    userMsg.className = 'chat-msg user-message';
    userMsg.innerHTML = '<div class="chat-msg-label">You</div>' + escapeHtml(message);
    chatBox.appendChild(userMsg);

    // Clear input immediately
    inputField.value = "";
    chatBox.scrollTop = chatBox.scrollHeight;

    // Show typing indicator
    const typingMsg = document.createElement('div');
    typingMsg.className = 'chat-msg assistant-message';
    typingMsg.innerHTML = '<div class="chat-msg-label">Assistant</div><span style="opacity:0.5;">Thinking...</span>';
    chatBox.appendChild(typingMsg);
    chatBox.scrollTop = chatBox.scrollHeight;

    try {
        const response = await fetch('https://localhost:57317/Chat/Send', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ text: message })
        });

        let result = await response.text();
        // Remove the last 5 characters from the result
        result = result.slice(0, -5);

        // Replace typing indicator with actual response
        typingMsg.innerHTML = '<div class="chat-msg-label">Assistant</div>' + escapeHtml(result);

    } catch (error) {
        console.error('Error:', error);
        typingMsg.innerHTML = '<div class="chat-msg-label">Assistant</div><span style="color:var(--text-danger);">Could not reach the AI assistant. Make sure the API is running.</span>';
    }

    chatBox.scrollTop = chatBox.scrollHeight;
}

function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}
