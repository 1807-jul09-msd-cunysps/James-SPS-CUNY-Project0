var product = [
    "iphoneX: 999.00",
    "Chips: 10.00",
    "Earphone: 100.00",
    "Case: 15.00",
    "gum: 10.00"
]

var sum = 0
for (item in product) {
//    alert(sum)
//    alert(product[item])
    sum += Number(product[item].split(':')[1]) //I'm not sure why this works without .trim()
}
alert(sum)