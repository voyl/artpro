var aesAddress = "192.168.42.207";
// We will use the unsecure socket. If you want to use the secure socket then change this port
// to 4722
var aesPort = "4721";
var appName = "IE Softphone";
var username = "selvi";
var password = "selvi12";
var switchName = "acm";

var sessionDuration = "180";
var sessionCleanupDelay = "20";
var protocolVersion = "http://www.ecma-international.org/standards/ecma-323/csta/ed3/priv1";
// Change isSecure to true if you are using the secure port (4722 by default)
var isSecure = false;
var startAutoKeepAlive = true;


var loginCode = "*14";
var auxCode = "*15";
var autoInCode = "*12";

//var wsURL = 'http://81.214.63.23:5001/GazIVR/GazIVRSorgulama.asmx/crmMusteriBulTelNo';
var wsURL = "http://localhost:60808/Utils.asmx/TestPopupURL"
var paramName = 'telno';








//arttech: edit below if needed to change AUX reason labels and reason codes

function LoadAUXList() {

    var data = [
        { text: 'Mola', value: '1' },
        { text: 'Yemek', value: '2' },
        { text: 'Backoffice', value: '3' },
        { text: 'Dış Arama', value: '4' },
        { text: 'Toplantı', value: '5' },
        { text: 'Sistem Kesinti', value: '6' },
        { text: 'Diğer', value: '' }
    ];

    var html = '';
    for (var i = 0; i < data.length; i++) {
        var d = data[i];
        html += '<a href="#" onclick="updateAUXStatus(' + d.value + ');" class="button orange">' + d.text + '</a>';
    }


    document.getElementById('lstAUX').innerHTML = html;

    /*var select = document.getElementById("drpAUX");

    select.options.length = 0; // clear out existing items
    for (var i = 0; i < data.length; i++) {
        var d = data[i];
        select.options.add(new Option(d.text, i))
    }*/
}
function getOnHookTime() {

    hookOnTime = new Date();
}


function getOffHookTime() {

    hookOnTime = new Date();
}


function getCallDuration() {

    var dif = hookOnTime.getTime() - hookOffTime.getTime()

    var seconds = dif / 1000;
    var seconds = Math.abs(seconds);

    var minutes = (Math.floor(seconds/60) < 10) ? "0" + Math.floor(seconds/60) : Math.floor(seconds/60);
    var seconds = (seconds % 60 > 9) ? seconds % 60 : "0" + seconds % 60;
    return minutes+":"+seconds;
}