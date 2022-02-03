function GetFormHash(){
    var hashString = location.hash.substring(1);
    //alert(hashString)
    var lines = [];
    var dot = hashString.indexOf(".");
    while(dot>=0){
        lines.push(hashString.substring(0,dot));
        hashString=hashString.substring(dot+1);
        dot = hashString.indexOf(".");
    }
    lines.push(hashString)
    //alert(lines.toString())
    for(ir=0;ir<lines.length;ir++){
        for(ic=0;ic<lines[ir].length;ic++){
            setBlock(ir,ic,lines[ir].substring(ic,ic+1)=="1")
            //alert(lines[ir].substring(ic,ic+1))
        }
    }
    MakeHash();
}


function MakeHash(){
    var outHash = "";
    for(ir=0;ir<gameSize;ir++){
        for(ic=0;ic<gameSize;ic++){
            if(blocks[ir][ic]){
                outHash=outHash+"1";
            }
            else{
                outHash=outHash+"0";
            }
        }
        outHash=outHash+".";
    }
    outHash=outHash.substring(0,outHash.length-1);
    location.hash=outHash;
}