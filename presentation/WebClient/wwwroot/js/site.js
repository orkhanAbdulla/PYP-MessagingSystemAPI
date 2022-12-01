let chatAbout = document.querySelector(".chat-header .chat-about h6")
let items = document.querySelectorAll("#plist .clearfix")
items.forEach(function (item) {
	item.addEventListener('click', function () {
		items.forEach(function (item) {
			item.classList.remove('active')
		});
		item.classList.add('active');
		chatAbout.innerText=""
		let name = item.lastElementChild.firstElementChild.innerText
		chatAbout.innerText=name
	})
})

//GetPostByConnectionId
let token = document.getElementById("token").value
let usernName = document.getElementById("userName").value


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
                console.log(createdBy)
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


///SignalR
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7055/chatHub").build();
connection.start()
console.log(connection)


