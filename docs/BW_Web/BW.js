var blocks = null;
var gameSize;

function initGame(size){
    blocks=[]
    gameSize=size;
    for(ir=0;ir<size;ir++){
        blocks[ir]=[];
        for(ic=0;ic<size;ic++){
            blocks[ir][ic]=false;
        }
    }
}

function buttonClick(r,c){
    //alert(gameSize);
    blockClick(r,c);
    MakeHash();
}

function blockClick(r,c){
    tryChange(r,c);
    tryChange(r+1,c);
    tryChange(r-1,c);
    tryChange(r,c+1);
    tryChange(r,c-1);
}

function tryChange(r,c){
    //alert("R"+r+"C"+c)
    if(r>=0 && c>=0 && r<gameSize && c<gameSize){
        //alert("tryOK");
        ChangeColor(r,c);
        blocks[r][c]= !blocks[r][c];
    }
}

function setBlock(r,c,value){
    SetColor(r,c,value);
    blocks[r][c]=value;
}

function randomClick(){
    //alert(0)
    clearGame();
    for(ir=0;ir<gameSize;ir++){
        for(ic=0;ic<gameSize;ic++){
            if(Math.random()<0.5){
                blockClick(ir,ic);
            }
        }
    }
    MakeHash();
}

function randomSet(){
    for(ir=0;ir<gameSize;ir++){
        for(ic=0;ic<gameSize;ic++){
            setBlock(ir,ic,Math.random()<0.5);
        }
    }
    MakeHash();
}

function clearGame(){
    //alert(0)
    for(ir=0;ir<gameSize;ir++){
        for(ic=0;ic<gameSize;ic++){
            setBlock(ir,ic,false);
        }
    }
    MakeHash();
}