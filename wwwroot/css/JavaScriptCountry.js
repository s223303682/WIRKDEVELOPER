var countrystatecityinfo = {
    SouthAfrica: {
        EasternCape: {
            PE: {
                Summerstrand: ["6001", "6002"],
                NorthEnd: ["7001", "7002"]
            },
            EastLondon: {
                Mdatsane: ["5001", "5002"],
                town: ["4001", "4002"]
            },
        },
        CapeTown: {
            Summerstrand2: ["6001", "6002"],
            NorthEnd2: ["7001", "7002"]
        },
        EastLondon: {
            Mdatsane2: ["5001", "5002"],
            town2: ["4001", "4002"]
        },
    },


}
    
   

window.onload = function () {
    const countryselection = document.getElementById('country'),
        provinceselection = document.getElementById('province'),
        cityselection = document.getElementById('city'),
        surbubselection = document.getElementById('surbub'),
        zipselection = document.getElementById('zip'),
        selection = document.querySelectorAll('select')


   

    provinceselection.disabled = true;
    cityselection.disabled = true;
    surbubselection.disabled = true;
    zipselection.disabled = true;

    selection.forEach(select=>{
        if (select.disabled == true) {
            select.style.cursor = "auto";
        }
        else {
            select.style.cursor = "auto";
        }

    })
    for (let country in countrystatecityinfo) {
        /*console.log(country)*/
        countryselection.options[countryselection.option.length] = new Option(country, country)
        countryselection.onchange = (e) => {
            provinceselection.disabled = false;
            cityselection.disabled = true;
            surbubselection.disabled = true;
            zipselection.disabled = true;

            selection.forEach(select => {
                if (select.disabled == true) {
                    select.tyle.cursor = "auto";
                }
                else {
                    select.style.cursor = "auto";
                }

            })
            provinceselection.length = 1;
            cityselection.length = 1;
            surbubselection.length = 1;
            zipselection.length = 1;

            for (let province in countrystatecityinfo[e.target.value]) {
                provinceselection.options[provinceselection.option.length] = new option(
                    province, province
                )
            }
        }
            provinceselection.onchange = (e) => {
                cityselection.disabled = false;
                surbubselection.disabled = true;
                zipselection.disabled = true;

                selection.forEach(select => {
                    if (select.disabled == true) {
                        select.style.cursor = "auto";
                    }
                    else {
                        select.style.cursor = "auto";
                    }

                })
                cityselection.length = 1;
                surbubselection.length = 1;
                zipselection.length = 1;


                for (let city in countrystatecityinfo[e.target.value]) {
                    cityselection.options[cityselection.option.length] = new option(
                        city, city
                    )
                }
            }
        cityselection.onchange = (e) => {
           
            surbubselection.disable = false;
            zipselection.disabled = true;

            selection.forEach(select => {
                if (select.disabled == true) {
                    select.style.cursor = "auto";
                }
                else {
                    select.style.cursor = "auto";
                }

            })
           
            surbubselection.length = 1;
            zipselection.length = 1;


            for (let surbub in countrystatecityinfo[e.target.value]) {
                surbubselection.options[surbubselection.option.length] = new option(
                    surbub, surbub
                )
            }
        }
 }

}

const notesContainer = document.querySelector(".note-container");
const createBtn = document.querySelector(".btn");
let notes = document.querySelectorAll(".input-box");

function shownotes() {
    notesContainer.innerHTML = localStorage.getItem("notes");
}
function updateStorage() {
    localStorage.setItem("notes", notesContainer.innerHTML);

}
shownotes();
createBtn.addEventListener("click", () => {
    let inputBox = document.createElement("p");
    let img = document.createElement("img");
    inputBox.className = "input-box";
    inputBox.setAttribute("contenteditable", "true");
    img.src = "~/Images/Surgeon/delete.jpeg";
    notesContainer.appendChild(inputBox).appendChild(img);
})
notesContainer.addEventListener("click", function (e) {
    if (e.target.tagName === "IMG") {
        e.target.parentElement.remove();
        updateStorage();
    }
    else if (e.target.tagName === "P") {
        notes = document.querySelectorAll(".input-box");
        notes.forEach(nt => {
            nt.onkeyup = function () {
                updateStorage();
            }
        })
    }


})
document.addEventListener("keydown", event => {
    if (event.key === "Enter") {
        document.execCommand("insertLineBreak");
        event.preventDefault();
    }
})