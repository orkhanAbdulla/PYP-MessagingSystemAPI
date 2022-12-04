let token = document.getElementById("token").value
let usernName = document.getElementById("userName").value
let chat = document.querySelector(".card .chat")

var chatAbout = document.querySelector(".chat-header .chat-about")
let items = document.querySelectorAll("#plist .clearfix")
items.forEach(function (item) {
	item.addEventListener('click', function () {
		items.forEach(function (item) {
			item.classList.remove('active')
		});
        item.classList.add('active');
		chatAbout.innerText=""
		let name = item.lastElementChild.firstElementChild.innerText
        chatAbout.innerText = name
        let connection = item.getAttribute('data-connection')
        chatAbout.setAttribute("data-connection-Id", connection)
        chat.setAttribute("id", connection);
       
	})
})

//GetPostByConnectionId


function GetPostByConnectionId(id) {
    var formData = {
        ConnectionId: id,
        ReactionsCount:3,
        RepliesCount:3
    }
    $.ajax({
        url: "https://localhost:7055/api/MessagingManager/GetPostsByConnectionId",
        type: 'GET',
        data: formData,
        dataType: "json",
        headers: { "Authorization": 'Bearer ' + token },
    }).done(function (data) {
        $(".chat .chat-history ul li").remove()
        data.map(({ id, message, createdAt, createdBy }) => {
            let date = new Date(createdAt)
            if (usernName != createdBy) {
                $(".chat .chat-history ul").append(
                    `
               <li class="clearfix" data-id="${id}">
               <div class="message-data text-right">
                    <span class="message-data-time">${createdBy}
                    </span>
                    <span class="message-data-time" style="font-size:10px">
                        ${date.toLocaleString('en-Us', { weekday: "long", month: "short", day: "numeric", hour: '2-digit', minute: '2-digit', }) }
                    </span>
                </div>
                        <div class="message other-message float-right">${message}</div>
                </li>
                `
                )
            }
            else {
                $(".chat .chat-history ul").append(
                `
                  <li class="clearfix" data-id="${id}">
                    <div class="message-data">
                        <span class="message-data-time">
                            Me
                        </span>
                        <span class="message-data-time" style="font-size:10px">
                        ${date.toLocaleString('en-Us', { weekday: "long", month: "short", day: "numeric", hour: '2-digit', minute: '2-digit', }) }
                        </span>
                    </div>
                    <div class="message my-message">${message}</div>
                </li>
                   `
                )
            }
        })
    });
}


// CreatePost
var inpudSend = document.getElementById("sendButton")
inpudSend.addEventListener("keypress", (e) => {
    if (e.key === 'Enter') {
        var connectionId = chatAbout.getAttribute('data-connection-Id')
        var message = inpudSend.value
        var formData = {
            ConnectionId: connectionId,
            Message: message,
        }
        $.ajax({
            url: "https://localhost:7055/api/MessagingManager/Post/Create",
            type: 'POST',
            data: formData,
            dataType: "json",
            headers: { "Authorization": 'Bearer ' + token },
        }).done(function ({ id, message, createdAt, }) {
            inpudSend.value = ""
            let date = new Date(createdAt)
            $(".chat .chat-history ul").append(
                `
                  <li class="clearfix">
                    <div class="message-data">
                        <span class="message-data-time">
                            Me
                        </span>
                        <span class="message-data-time" style="font-size:10px">
                        ${date.toLocaleString('en-Us', { weekday: "long", month: "short", day: "numeric", hour: '2-digit', minute: '2-digit', })}
                        </span>
                    </div>
                    <div class="message my-message">${message}</div>
                </li>
                   `
            )
        
        })
    }

})




/*console.log(elm.textContent="online")*/
///SignalR
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7055/chatHub", { accessTokenFactory: () => token }).build();
connection.start()

connection.on("ReceiveMessage", function (connectionId,message, userName, CreatedAt) {
    let date = new Date(CreatedAt)
    console.log(connectionId)
    document.getElementById(connectionId).children[1]
        .firstElementChild.innerHTML += `
           <li class="clearfix">
               <div class="message-data text-right">
                  <span class="message-data-time">${userName}
                  </span>
                  <span class="message-data-time" style="font-size:10px">
                     ${date.toLocaleString('en-Us', { weekday: "long", month: "short", day: "numeric", hour: '2-digit', minute: '2-digit', })}
                 </span>
                </div>
                    <div class="message other-message float-right">${message}</div>
             </li>
      `
})



connection.on("UserConnected", function (userName) {
    let elm = document.getElementById(userName)
    elm.previousElementSibling.classList.remove("offline")
    elm.previousElementSibling.classList.add("online")
    elm.innerText = ""
    elm.innerText = "online"
 
})
connection.on("UserDisConnected", function (userName) {
    let elm = document.getElementById(userName)
    elm.previousElementSibling.classList.remove("online")
    elm.previousElementSibling.classList.add("offline")
    elm.innerText = ""
    elm.innerText = "offline"
    
})


