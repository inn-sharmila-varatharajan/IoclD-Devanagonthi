let getDate = () => {
    let date = new Date();
    let mm = ("0" + (date.getMonth() + 1)).slice(-2);
    let dd = ("0" + date.getDate()).slice(-2);
    var shopdatas = "";
    return `${date.getFullYear()}-${mm}-${dd}`;
}
async function loadCarosel() {
    const api = api_carosel_rph + getDate();
    try {
        const response = await fetch(api);
        if (!response.ok) {
            const message = `An error has occured: ${response.status}`;
            throw new Error(message);
        }

        const data = await response.json();
        data.forEach((obj) => {
            let carosel_id = "grp_carosel_" + obj.carousel;
            let colorCode = {
                0: { name: "green", fill: "#2ecc71", stroke: "#27ae60", text: "#fff" },
                1: { name: "amber", fill: "#00FFFF", stroke: "#00FFFF", text: "#fff" },
                2: { name: "red", fill: "#e74c3c", stroke: "#c0392b", text: "#fff" },
            }
            let rphVal = obj.rph > 999 ? obj.rph : "&nbsp;" + obj.rph
            var hh = "ATC:" + rphVal * 16;
            $("#" + carosel_id + " .carosel-text").html(rphVal).css({ 'fill': colorCode[obj.status]["text"] });
           
            $("#" + carosel_id + " .carosel-circle").css({ 'fill': colorCode[obj.status]["fill"], 'stroke': colorCode[obj.status]["stroke"] });
            $("#" + carosel_id + " .carosel-circle").mouseover(function () { show_popup(carosel_id, obj.livecount, obj.carousel, obj.status); });
            $("#" + carosel_id + " .carosel-circle").mouseout(function () { show_popupin(carosel_id, rphVal, obj.carousel, obj.status); });

        })
    } catch (error) {
        console.log(error)
    }
}

function show_popupin(id, count, carousel, status) {
    let colorCode = {
        0: { name: "green", fill: "#2ecc71", stroke: "#27ae60", text: "#fff" },
        1: { name: "amber", fill: "#00FFFF", stroke: "#00FFFF", text: "#fff" },
        2: { name: "red", fill: "#e74c3c", stroke: "#c0392b", text: "#fff" },
    }

    $("#" + id + " .carosel-text").html(count).css({ 'fill': colorCode[status]["text"] });

}

function show_popup(id, count, carousel,status) {

    let colorCode = {
        0: { name: "green", fill: "#2ecc71", stroke: "#27ae60", text: "#fff" },
        1: { name: "amber", fill: "#00FFFF", stroke: "#00FFFF", text: "#fff" },
        2: { name: "red", fill: "#e74c3c", stroke: "#c0392b", text: "#fff" },
    }

    

    $("#" + id + " .carosel-text").html("LC:" + count + "<wbr>" + "CR:" + carousel);//.css({ 'fill': colorCode[obj.status]["text"] });
}


function show_popuptext(id, count, carousel, status) {

    let colorCode = {
        0: { name: "green", fill: "#2ecc71", stroke: "#27ae60", text: "#fff" },
        1: { name: "amber", fill: "#00FFFF", stroke: "#00FFFF", text: "#fff" },
        2: { name: "red", fill: "#e74c3c", stroke: "#c0392b", text: "#fff" },
    }
   // $( id + " .counter-group").html("LC:" + count + "<wbr>" + "CR:" + carousel);
    const tooltip = bootstrap.Tooltip.getInstance(id);
    if (!!tooltip) {
        tooltip.setContent({ '.tooltip-inner': count  });
    }

   
}

function show_popuptextout(id, count, carousel, status) {

    let colorCode = {
        0: { name: "green", fill: "#2ecc71", stroke: "#27ae60", text: "#fff" },
        1: { name: "amber", fill: "#00FFFF", stroke: "#00FFFF", text: "#fff" },
        2: { name: "red", fill: "#e74c3c", stroke: "#c0392b", text: "#fff" },
    }
    $( id + " .counter-group").html();
    const tooltip = bootstrap.Tooltip.getInstance(id);
    if (!!tooltip) {
        tooltip.setContent({ '.tooltip-inner': count  });
    }


}



async function loadDeviceDetails() {
    const api = api_deviceDetail + getDate();
    try {
        const response = await fetch(api);
        if (!response.ok) {
            const message = `An error has occured: ${response.status}`;
            throw new Error(message);
        }

        const data = await response.json();
        $(".counter-group .logo").addClass("logo-red")
        data.forEach((obj) => {
            $(".counter-group#g" + obj.DeviceId + " .logo").removeClass("logo-red")
            const tooltip = bootstrap.Tooltip.getInstance("#g" + obj.DeviceId)
            if (!!tooltip) {
                tooltip.setContent({ '.tooltip-inner': obj["eventcode40"] })
            } else {
              //  console.log(JSON.stringify(obj))
            }
           // show_popup(031,100,2);
        })
    } catch (error) {
        console.log(error)
    }
}
async function loadFloorData() {
    const api = api_floorData + getDate();
  try{
    const response = await fetch(api);
    if(!response.ok){
      const message = `An error has occured: ${response.status}`;
      throw new Error(message);
    }

      const data = await response.json();
      shopdatas = data;
      $(".carousel-track").removeClass("track-alert").removeClass("track-warning");
      $(".counter-group .logo").addClass("logo-red")
      $(".btn-deviator-grp").show();
      $(".btn-deviator-grp .deviator-arrow").hide();

    data.forEach((obj)=>{
        const ids = ("0" + obj.deviceid).slice(-3);
        const trackId = "#t" + ids;
        const iconid = "#g" + ids;
        const deviatorGrpId = "#gbtn" + ids;
       
            $(deviatorGrpId + " .deviator-" + obj.direction).show();
        
        //if ($(deviatorGrpId).length) {
        //    $(deviatorGrpId + " .deviator-" + obj.direction).show();
        //}
       //   else{
       //  console.log(deviatorGrpId)
       //}
        let className = "";
        if (obj.status == 3) {
            className = "track-danger";
        } else if (obj.status == 2) {
            className = "track-warning";
        } else if (obj.status == 1) {
            className = "track-alert";
        } else if (obj.status == 0) {
            className = "track-success";
        }


        $(trackId).addClass(className)
        $(trackId + "_1").addClass(className)
        $(trackId + "_2").addClass(className)
        $(".counter-group" + iconid + " .logo").removeClass("logo-red")
        const tooltip = bootstrap.Tooltip.getInstance(iconid)
        if (!!tooltip) {
            tooltip.setContent({ '.tooltip-inner': "" + obj.rpm })
        } 

        
    })
  } catch(error){
    console.log(error)
  }
}

async function loadtruckData() {
    const api = api_truckData;
    try {
        const response = await fetch(api);
        if (!response.ok) {
            const message = `An error has occured: ${response.status}`;
            throw new Error(message);
        }

        const data = await response.json();
        $(".truck").hide();
        $(".vid").hide();
       

        data.forEach((obj) => {
            const ids = ("0" + obj.device).slice(-3);
            const truckId = "#truck_" + ids+"_1";
            const vid = "#V" + ids;
          
          //  console.log(obj)
            if (obj.status == "0" || obj.status == "4" || obj.status=="1") {
               
                if ($(truckId).length) {
                    $(truckId).show();
                    $(vid).show();
                    $(vid).text(obj.vehnum);
                }
            }
            else {
                if ($(truckId).length) {
                    $(truckId).hide();
                    $(vid).text("");
                }
            }
            });
    
    } catch (error) {
        console.log(error)
    }
}

async function loaddirection(id) {
  
  
    const api = api_loaddirection + id;
    try {
        const response = await fetch(api);
        if (!response.ok) {
            const message = `An error has occured: ${response.status}`;
            throw new Error(message);
        }

        const data = await response.json();

        loadFloorData();

    } catch (error) {
        console.log(error)
    }
}


  
     
