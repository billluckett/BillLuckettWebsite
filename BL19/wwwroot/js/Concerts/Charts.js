// Concerts by Artist, column chart
function prepArtists() {
    var artists = [];
    var dataArray = [["Artist", "Shows Attended"]];

    // go through each show, find unique artists, count each instance of every artist
    for (var i = 0; i < shows.length; i++) {
        for (var j = 0; j < shows[i].artists.length; j++) {
            var artist = shows[i].artists[j].name;
            var idx = artists.indexOf(artist) + 1;
            if (idx == 0) {
                artists[artists.length] = artist;
                dataArray[dataArray.length] = [artist, 1]; // add new artist with shows-seen count of 1
            } else {
                dataArray[idx][1]++; // add to the count for that artist
            }
        }
    }

    filterData(dataArray, 2); // eliminate those with counts < 2
    dataArray.sort(function (a, b) { return b[1] - a[1]; });
    return dataArray;
}

// Concerts by Companion, column chart
function prepPeople() {
    var people = [];
    var dataArray = [["Person", "Shows Attended"]];

    // go through each show, find unique people, count each instance of every person
    for (var i = 0; i < shows.length; i++) {
        for (var j = 0; j < shows[i].people.length; j++) {
            var person = shows[i].people[j].name;
            var idx = people.indexOf(person) + 1;
            if (idx == 0) {
                people[people.length] = person;
                dataArray[dataArray.length] = [person, 1]; // add new person with shows-seen count of 1
            } else {
                dataArray[idx][1]++; // add to the count for that person
            }
        }
    }

    filterData(dataArray, 2); // eliminate those with counts < 2
    dataArray.sort(function (a, b) { return b[1] - a[1]; });
    return dataArray;
}

// Concerts by Venue, column chart
function prepVenues() {
    var venues = [];
    var dataArray = [["Venue", "Shows Attended"]];

    // go through each show, find unique venues, count each instance of every venue
    for (var i = 0; i < shows.length; i++) {
        var venue = shows[i].venue.name;
        var idx = venues.indexOf(venue) + 1;
        if (idx == 0) {
            venues[venues.length] = venue;
            dataArray[dataArray.length] = [venue, 1]; // add new venue with shows-seen count of 1
        } else {
            dataArray[idx][1]++; // add to the count for that venue
        }
    }

    filterData(dataArray, 2); // eliminate those with counts < 2
    dataArray.sort(function (a, b) { return b[1] - a[1]; });
    return dataArray;
}

// Concerts by State, column chart
function prepStates() {
    var states = [];
    var dataArray = [["State", "Shows Attended"]];

    // go through each show, find unique states, count each instance of every state
    for (var i = 0; i < shows.length; i++) {
        var state = shows[i].state;
        var idx = states.indexOf(state) + 1;
        if (idx == 0) {
            states[states.length] = state;
            dataArray[dataArray.length] = [state, 1]; // add new state with shows-seen count of 1
        } else {
            dataArray[idx][1]++; // add to the count for that state
        }
    }

    dataArray.sort(function (a, b) { return b[1] - a[1]; });
    return dataArray;
}

// Concerts by Era, column chart
function prepEras() {
    var eras = [];
    var dataArray = [["Era", "Shows Attended"]];

    // go through each show, find unique eras, count each instance of every era
    for (var i = 0; i < shows.length; i++) {
        var era = shows[i].era;
        var idx = eras.indexOf(era) + 1;
        if (idx == 0) {
            eras[eras.length] = era;
            dataArray[dataArray.length] = [era, 1]; // add new era with shows-seen count of 1
        } else {
            dataArray[idx][1]++; // add to the count for that era
        }
    }

    return dataArray;
}

// Concerts by Year, column chart
function prepYears() {
    var years = [];
    var dataArray = [["Year", "Shows Attended"]];

    // go through each show, find unique years, count each instance of every year
    for (var i = 0; i < shows.length; i++) {
        var date = new Date(shows[i].date);
        var year = date.getFullYear().toString();
        var idx = years.indexOf(year) + 1;
        if (idx == 0) {
            years[years.length] = year;
            dataArray[dataArray.length] = [year, 1]; // add new year with shows-seen count of 1
        } else {
            dataArray[idx][1]++; // add to the count for that year
        }
    }

    dataArray.sort(function (a, b) { return a[0] - b[0]; });

    // IF WE WANNA add years with 0 shows attended
    var yfirst = parseInt(dataArray[1][0]); // 1989
    var ylast = parseInt(dataArray[dataArray.length - 1][0]); // 2014
    for (var i = 2; i < dataArray.length; i++) {
        while (parseInt(dataArray[i][0]) - yfirst > 1) {
            dataArray[dataArray.length] = [++yfirst, 0];
        }
        yfirst++;
    }

    // IF WE WANNA add years between last show attended and current year
    while ((new Date()).getFullYear() > ylast) {
        dataArray[dataArray.length] = [++ylast, 0];
    }

    dataArray.sort(function (a, b) { return a[0] - b[0]; });
    return dataArray;
}

// Concerts by Month, column chart
function prepMonths() {
    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    //var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
    var dataArray = [["Month", "Shows Attended"]];

    for (var i = 0; i < months.length; i++) {
        dataArray[dataArray.length] = [months[i], 0];
    }

    // go through each show, find unique months, count each instance of every month
    for (var i = 0; i < shows.length; i++) {
        var date = new Date(shows[i].date);
        dataArray[date.getMonth() + 1][1]++; // add to the count for that month
    }

    return dataArray;
}

// eliminate items in "arrData" with counts < "count"
function filterData(arrData, count) {
    for (var i = arrData.length - 1; i >= 0; i--) {
        if (arrData[i][1] < count) {
            arrData.splice(i, 1);
        }
    }
}

// list of charts and chart-prep functions
var charts = [
    { name: "Artists", func: prepArtists, title: "Concerts Attended, by Artist (minimum 2)", smText: true },
    { name: "People", func: prepPeople, title: "Concerts Attended, by Companion (minimum 2)", smText: true },
    { name: "Venues", func: prepVenues, title: "Concerts Attended, by Venue (minimum 2)", smText: true },
    { name: "States", func: prepStates, title: "Concerts Attended, by State", smText: false },
    { name: "Eras", func: prepEras, title: "Concerts Attended, by Era", smText: false },
    { name: "Years", func: prepYears, title: "Concerts Attended, by Year", smText: true },
    { name: "Months", func: prepMonths, title: "Concerts Attended, by Month", smText: true },
];
