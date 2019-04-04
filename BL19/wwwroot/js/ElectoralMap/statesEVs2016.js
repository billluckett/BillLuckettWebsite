/*
var groups = [
	{ name: "Solid Dem", color: "#00a" },
	{ name: "Likely Dem", color: "#12f" },
	{ name: "Lean Dem", color: "#56f" },
	{ name: "Toss-Up", color: "violet" }, // "violet" or "darkmagenta" or "#d8c" or "chartreuse"
	{ name: "Lean Rep", color: "#f65" },
	{ name: "Likely Rep", color: "#f11" },
	{ name: "Solid Rep", color: "#d00" }
];
*/

// changed 11/6/16:
var groups = [
	{ name: "Solid Dem", color: "#12f" },
	{ name: "Likely Dem", color: "#45f" }, // #68f
	{ name: "Lean Dem", color: "#68f" },
	{ name: "Toss-Up", color: "gold" }, // "violet", "darkmagenta", "#d8c", "chartreuse", "lightgray", "lemonchiffon"
	{ name: "Lean Rep", color: "#f86" },
	{ name: "Likely Rep", color: "#f54" }, // #f11
	{ name: "Solid Rep", color: "#f11" }
];

// OTHER SCHEMES OR IDEAS:
// #00a, #12f, #56f, #d7c or violet or chartreuse, #f65, #f11, #d00
// darkblue, blue, lightblue, purple, pink, red, darkred
// blue, blue, blue, chartreuse, red, red, red


var statesEVs2016 = [
	{ name: "AK", rating: 6, votes: 3 },
	{ name: "AL", rating: 6, votes: 9 },
	{ name: "AR", rating: 6, votes: 6 },
	{ name: "AZ", rating: 3, votes: 11 },
	{ name: "CA", rating: 0, votes: 55 },
	{ name: "CO", rating: 0, votes: 9 },
	{ name: "CT", rating: 0, votes: 7 },
	{ name: "DC", rating: 0, votes: 3 },
	{ name: "DE", rating: 0, votes: 3 },
	{ name: "FL", rating: 3, votes: 29 },
	{ name: "GA", rating: 4, votes: 16 },
	{ name: "HI", rating: 0, votes: 4 },
	{ name: "IA", rating: 3, votes: 6 },
	{ name: "ID", rating: 6, votes: 4 },
	{ name: "IL", rating: 0, votes: 20 },
	{ name: "IN", rating: 5, votes: 11 },
	{ name: "KS", rating: 6, votes: 6 },
	{ name: "KY", rating: 6, votes: 8 },
	{ name: "LA", rating: 6, votes: 8 },
	{ name: "MA", rating: 0, votes: 11 },
	{ name: "MD", rating: 0, votes: 10 },
	{ name: "ME", rating: 1, votes: 4 },
	{ name: "MI", rating: 1, votes: 16 },
	{ name: "MN", rating: 1, votes: 10 },
	{ name: "MO", rating: 5, votes: 10 },
	{ name: "MS", rating: 6, votes: 6 },
	{ name: "MT", rating: 6, votes: 3 },
	{ name: "NC", rating: 3, votes: 15 },
	{ name: "ND", rating: 6, votes: 3 },
	{ name: "NE", rating: 6, votes: 5 },
	{ name: "NH", rating: 2, votes: 4 },
	{ name: "NJ", rating: 0, votes: 14 },
	{ name: "NM", rating: 0, votes: 5 },
	{ name: "NV", rating: 2, votes: 6 },
	{ name: "NY", rating: 0, votes: 29 },
	{ name: "OH", rating: 3, votes: 18 },
	{ name: "OK", rating: 6, votes: 7 },
	{ name: "OR", rating: 0, votes: 7 },
	{ name: "PA", rating: 2, votes: 20 },
	{ name: "RI", rating: 0, votes: 4 },
	{ name: "SC", rating: 5, votes: 9 },
	{ name: "SD", rating: 6, votes: 3 },
	{ name: "TN", rating: 6, votes: 11 },
	{ name: "TX", rating: 5, votes: 38 },
	{ name: "UT", rating: 6, votes: 6 },
	{ name: "VA", rating: 2, votes: 13 },
	{ name: "VT", rating: 0, votes: 3 },
	{ name: "WA", rating: 0, votes: 12 },
	{ name: "WI", rating: 2, votes: 10 },
	{ name: "WV", rating: 6, votes: 5 },
	{ name: "WY", rating: 6, votes: 3 }
];

function set(name, rating) {
    var n = statesEVs2016.length;
    for (var i = 0; i < n; i++) {
        if (statesEVs2016[i].name == name) {
            statesEVs2016[i].rating = rating;
            break;
        }
    }
}

function setMany(states, rating) {
    for (var i = 0; i < states.length; i++) {
        set(states[i], rating);
    }
}

function setAll(rating) {
    for (var i = 0; i < statesEVs2016.length; i++) {
        set(statesEVs2016[i].name, rating);
    }
}

// changes 10/13/16:
setMany(["AZ", "FL", "IA", "NC", "NV", "OH"], 2);
setMany(["GA", "IN", "SC", "UT"], 3);

// changes 11/6/16:
setMany(["IN", "MO"], 6);
setMany(["MT", "UT", "AK", "TX", "GA", "SC"], 5);
setMany(["AZ", "IA", "OH"], 4);
setMany(["FL", "NC", "NV"], 3);
setMany(["CO", "NH", "VA"], 2);
setMany(["ME", "MI", "MN", "PA", "WI"], 1);

// second stab, 11/6/16
setMany(["NH", "OH"], 3);

// electoralvotemap.com, ~3/11/18
setAll(6);
setMany([
    "HI", "WA", "OR", "CA", "NV",
    "CO", "NM", "MN", "IL", "NY",
    "VT", "MA", "CT", "RI", "NJ",
    "DE", "DC", "MD", "VA", "ME"
], 0);
setMany([
    "AZ", "WI", "MI",
    "PA", "NH", "NC", "FL"
], 3);

// changes 3/24/18
setMany(["AK", "IA", "OH", "TX"], 5);
setMany(["GA"], 4);
setMany(["NV", "VA"], 2);
setMany(["CO", "MN"], 1);
