let editor;

window.onload = function () {
    editor = ace.edit("editor");
    editor.setTheme("ace/theme/monokai");
    editor.session.setMode("ace/mode/python");
}


async function executePythonCode() {
    const pythonCode = document.getElementById('editor').innerText; 
    const url = 'https://localhost:57317/api/CodeExecution/execute'; 
    const payload = {
        language: 'python',
        code: pythonCode
    };

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Error: ${errorText}`);
        }

        const contentType = response.headers.get('Content-Type');

        if (contentType.includes('image/png')) {
            const blob = await response.blob();
            const imageUrl = URL.createObjectURL(blob);
            displayImage(imageUrl);
        } else {
            const text = await response.text();
            displayOutput(text);
        }
    } catch (error) {
        console.error('Error executing Python code:', error);
        displayError(error.message);
    }
}

function displayOutput(output) {
    const outputElement = document.getElementById('output');
    outputElement.innerText = output;
}

function displayImage(imageUrl) {
    const outputElement = document.getElementById('output');
    outputElement.innerHTML = `<img src="${imageUrl}" alt="Python execution output">`;
}

function displayError(errorMessage) {
    const outputElement = document.getElementById('output');
    outputElement.innerText = errorMessage;
}



/*
function executePythonCode() {
    const code = document.getElementById('codeEditor').value;

    fetch('https://localhost:57317/api/CodeExecution/execute', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            language: 'python',
            code: code
        })
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => { throw new Error(text) });
            }
            const contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("image")) {
                return response.blob();
            } else {
                return response.text();
            }
        })
        .then(result => {
            const outputElement = document.getElementById('output');
            if (result instanceof Blob) {
                const url = URL.createObjectURL(result);
                outputElement.innerHTML = `<img src="${url}" alt="Quantum Circuit">`;
            } else {
                outputElement.innerHTML = `<pre>${result}</pre>`;
            }
        })
        .catch(error => {
            const outputElement = document.getElementById('output');
            outputElement.innerHTML = `<p>Error: ${error.message}</p>`;
        });
}

*/
