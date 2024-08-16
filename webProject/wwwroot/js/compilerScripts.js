async function sendMessage() {
    const inputField = document.getElementById('chat-input');
    const chatBox = document.getElementById('chat-box');
    const message = inputField.value;

    if (message.trim() === "") return;

    // Display the user's message in the chat box
    const userMessage = document.createElement('div');
    userMessage.className = 'user-message';
    userMessage.textContent = "User: " + message;
    chatBox.appendChild(userMessage);

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

        // Display the assistant's response in the chat box
        const assistantMessage = document.createElement('div');
        assistantMessage.className = 'assistant-message';
        assistantMessage.textContent = result;
        chatBox.appendChild(assistantMessage);

    } catch (error) {
        console.error('Error:', error);
    }

    // Clear the input field
    inputField.value = "";
    // Scroll to the bottom of the chat box
    chatBox.scrollTop = chatBox.scrollHeight;
}


