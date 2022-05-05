function showToast(message, status = "#FFFFFF", dismissible = true, duration = 5000) {
    Snackbar.show({
        actionTextColor: status,
        pos: 'bottom-center',
        text: message,
        actionText: 'Dismiss',
        width: 'auto',
        duration: duration,
        textColor: "#FFFFFF",
        showAction: dismissible,
        backgroundColor: '#323232'
    })
}

function copy(content) {
    navigator.clipboard.writeText(content)
        .then(() => {
            showToast("Copied", "success");
        })
        .catch(err => {
            showToast("Something went wrong while copying to clipboard.", "danger")
            console.error(err)
        })
}

$(document).ready(() => {
    $('[data-toggle="tooltip"]').tooltip()
    $('[data-toggle="popover"]').popover()

    $("#copy-year").text(new Date().getFullYear())
})

$("#typeSelector").change(() => {
    if ($("#typeSelector").val() == 1) {
        $("#savingPlace").show(100)
    } else {
        $("#savingPlace").hide(100)
    }
})

let sidebarMenu = $("#sidebarMenu")
let fullOverlay = $("#full-overlay")

function openNav() {
    sidebarMenu.css("left", "0")
    fullOverlay.css("display", "block")

    $(document.body).css("overflow", "hidden")
}

function closeNav() {
    sidebarMenu.css("left", "-300px")
    fullOverlay.css("display", "none")

    $(document.body).css("overflow", "auto")
}

fullOverlay.click(closeNav)

/* SignalR
-------------------------------------------------- */

var connection 

function StartHub() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/bankHub")
        .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
        .configureLogging(signalR.LogLevel.Warning)
        .withAutomaticReconnect()
        .build()

    async function startHub() {
        try {
            await connection.start()
        } catch (err) {
            console.log(err)
            setTimeout(startHub, 5000)
        }
    }

    connection.onclose(async () => { await startHub() })

    startHub()

    connection.on('BadRequest', (message) => { showToast(message, "red") });
    connection.on('Successful', (message, addData) => { showToast(message, "success") });

    connection.on('Result', (data) => {
        $("#reciever-name").text(data.OwnerName)
        $("#reciever-account-number").text(data.AccountNumber)
        $("#reciever-email").text(data.OwnerEmail)
        $("#reciever-email").attr("href", "mailto:" + data.OwnerEmail)
    })
}

$("#card-input").change(() => {
    if ($("#card-input").val().length >= 8) {
        connection.invoke('GetAccountOwner', $("#card-input").val().replace("-", "").replace("-", "")).catch(err => {
            console.error(err.toString())
            showToast("Something went wrong, try again!", "red")
        })
    }
})

$("#transfer-btn").click(() => {
    if ($("#card-input").val().length >= 8 && $("#card-amount").val().length > 1) {
        connection.invoke('Transfer', $("#card-input").val().replace("-", "").replace("-", ""), $("#card-amount").val()).catch(err => {
            console.error(err.toString())
            showToast("Something went wrong, try again!", "red")
            return
        })

        $("#card-input").val("")
        $("#card-amount").val("")

        return
    }

    showToast("Fill all required fields!", "red")
})

$("#pay-service").click(() => {
    if ($("#payment-service").val() != 0) {

        var splitted = $("#payment-service").val().split("|");

        console.log(splitted)

        connection.invoke('Payment', splitted[0]).catch(err => {
            console.error(err.toString())
            showToast("Something went wrong, try again!", "red")
            return
        })

        return
    }

    showToast("Fill all required fields!", "red")
})

$("#payment-service").change(() => {
    var splitted = $("#payment-service").val().split("|");
    $("#payment-cost").val(splitted[1])
})

$("#lift-btn").click(() => {
    if ($("#pretended-amount").val().length > 0 && $("#pretended-amount").val() != 0) {
        connection.invoke('Lifting', $("#pretended-amount").val()).catch(err => {
            console.error(err.toString())
            showToast("Something went wrong, try again!", "red")
            return
        })

        $("#pretended-amount").val("")
        return
    }

    showToast("Insert a valid amount!", "red")
})

$("#next-btn").click(() => {
    $("#next-page").css("display", "block")
    $("#next-page").css("width", "100%")

    $("#prev-page").css("width", "0")
    $("#prev-page").css("display", "none")
})