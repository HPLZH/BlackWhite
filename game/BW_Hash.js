function GetFormHash() {
    var hashString = location.hash.substring(1);
    var lines = hashString.split(".");
    for (ir = 0; ir < lines.length; ir++) {
        for (ic = 0; ic < lines[ir].length; ic++) {
            setBlock(ir, ic, lines[ir].substring(ic, ic + 1) == "1")
        }
    }
    MakeHash();
}

function MakeHash() {
    var outHash = "";
    for (ir = 0; ir < gameSize; ir++) {
        var temp = "";
        for (ic = 0; ic < gameSize; ic++) {
            if (blocks[ir][ic]) {
                temp = temp + "1";
            }
            else {
                temp = temp + "0";
            }
        }
        outHash = outHash + temp + ".";
    }
    outHash = outHash.substring(0, outHash.length - 1);
    location.hash = outHash;
}
