var requestURL = 'https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json';
var request = new XMLHttpRequest(); 
request.open('GET', requestURL);
request.responseType = 'json';
request.send();
request.onload = function () {
    var superHeroes = request.response;
    for (var x in superHeroes) {
        alert(x+": "+superHeroes[x]);
    }
//    document.createElement(h1, superHeroes['squadName']);
    populateHeader(superHeroes);
    showHeroes(superHeroes);
}
