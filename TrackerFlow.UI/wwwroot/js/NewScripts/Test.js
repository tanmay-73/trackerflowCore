function officeLoop(c_ofcid) {
    for (var i = 0; i < office.length; i++) {
        if (office[i].c_ofcid == c_ofcid) {
            return office[i].c_ofcname;
        }
    }
}

function floorloop(c_floorid) {
    for (var i = 0; i < floor.length; i++) {
        if (floor[i].c_floorid == c_floorid) {
            return floor[i].c_floornum;
        }
    }
}