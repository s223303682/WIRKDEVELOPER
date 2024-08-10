const provincestate = {
    EasternCape: {
        PE: {
            "Summerstrand": ["6001", "6002"],
            "North End": ["7001", "7002"]
        },
        EastLondon: {
            "Mdatsane": ["5001", "5002"],
            "town": ["4001", "4002"]
        },
    },
    CapeTown: {
        "Summerstrand2": ["6001", "6002"],
        "North End2": ["7001", "7002"]
    },
    EastLondon: {
        "Mdatsane2": ["5001", "5002"],
        "town2": ["4001", "4002"]
    },
};

window.onload = function () {
    const provinceselection = document.querySelector("#province"),
        cityselection = document.querySelector("#city"),
        surbubselection = document.querySelector("#surbub");

    // console.log(provinceselection);

    cityselection.disabled = true;
    surbubselection.disabled = true;
    cityselection.length = 1;
    surbubselection.length = 1;

    for (let province in provincestate) {
        provinceselection.options[provinceselection.options.length] = new option(
            province, province
        );
    }
    provinceselection.onchange = (e) => {
        cityselection.disabled = false;

        cityselection.length = 1;
        surbubselection.length = 1;

        for (let city in provincestate[e.target.value]) {
            cityselection.options[cityselection.options.length] = new option(
               city ,city
            );
        }
    }

    cityselection.onchange = (e) => {
        cityselection.disabled = false;

        cityselection.length = 1;
        surbubselection.length = 1;

        for (let surbub in provincestate[provinceselection.value][e.target.value]) {
            surbubselection.options[surbubselection.options.length] = new option(
                surbub, surbub
            );
        }
    }
   


};
