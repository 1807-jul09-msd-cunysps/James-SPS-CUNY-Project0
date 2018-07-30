var requestURL = 'https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json';
var request = new XMLHttpRequest(); 
request.open('GET', requestURL);
request.responseType = 'json';
request.send();
request.onload = function () {
    var superHeroes = request.response;
    //for (var x in superHeroes) {
    //    alert(x+": "+superHeroes[x]);
    //}
    //var h = document.createElement('h1');
    //h.textContent = superHeroes['squadName'];
    //header.appendChild(h);
    populateHeader(superHeroes);
    showHeroes(superHeroes);
}

function showHeroes(superHeroes) {
    for (el in superHeroes.members) {
        showHero(superHeroes.members[el]);
    }
}

function showHero(member) {
    var a = document.createElement('article');
    var i = 0;
    for (el in member) {
        var h;
        if (i++ == 0) {
            h = document.createElement('h2');
        }
        else {
            h = document.createElement('p');
        }
        h.textContent = el + ': ' + member[el];
        a.appendChild(h);
    }
    //for (ele in s)

    section.appendChild(a);
}

function populateHeader(superHeroes) {
    var h = document.createElement('h1');
    h.textContent = superHeroes['squadName'];
    header.appendChild(h);

    var p = document.createElement('p');
    //p.textContent = "hello world";
    p.textContent = "// ";
    var i = 0;
    for (el in superHeroes) {
        if (i == 0) { }
        else { p.textContent += el + ": " + superHeroes[el] + " // " };
        i++
        if (i == 3) {break}
    }
    //p.textContent = p.textContent[0:34];
    header.appendChild(p);
}

