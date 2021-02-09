    export function getDateTime() {

        let days = ["Sunday", "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday"];
    
        let d = new Date();
        let day = days[d.getDay()];
        let hr = d.getHours();
        let min = d.getMinutes();
        let resMin;
        let ampm = "am";
    
        if (min < 10) {
            resMin = "0" + min;
        }
    
        else {
            resMin = min;
        }
    
        if (hr > 12) {
            hr -= 12;
            ampm = "pm";
        }
    
        let date = d.getDate();
        let month = d.getMonth() + 1;
        let resMonth;
        let year = d.getFullYear();
    
        if (month < 10) {
            resMonth = "0" + month;
        }
        else
        {
            resMonth = month;
        }
    
        return day + " " + resMonth + "-" + date + "-" + year + " " + hr + ":" + resMin +
            ampm + " - ";
    }
    
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

    export function logSomething(someText) {
        console.log("I`m logging some staff!");
    }

