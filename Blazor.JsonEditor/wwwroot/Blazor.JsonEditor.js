window.CopyToClipboard = (txt) => {
    var el = document.createElement('textarea');
    el.value = txt;
    el.setAttribute('readonly', '');
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
}