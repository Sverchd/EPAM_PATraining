export function getQueryParams(qs) {
    qs = qs.split("+").join(" ");
    
    const params = {},
        re = /[?&]?([^=]+)=([^&]*)/g;
    let tokens = re.exec(qs);

    while (tokens) {
        params[decodeURIComponent(tokens[1])] =
            decodeURIComponent(tokens[2]);
        tokens = re.exec(qs);
    }

    return params;
}