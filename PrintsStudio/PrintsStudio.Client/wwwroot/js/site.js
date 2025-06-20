function showAlert(message) {
    alert(message);
}
// Function to download file
function downloadFile(filename, byteArray) {
    const blob = new Blob([byteArray], { type: 'application/octet-stream' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// Function to copy to clipboard
function copyToClipboard(text) {
    navigator.clipboard.writeText(text);
}