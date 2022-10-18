
var connection = new signalR.HubConnectionBuilder().withUrl("/Dictionary/NotificationHub").build();
            connection.start().then(() => { //alert("connected")
    }
    );
connection.on("ReciveQuestions", function (message) {
    var notificationsCount = document.getElementById("notificationCount").innerHTML;
    notificationsCount = parseInt(notificationsCount) + 1;
    document.getElementById("notificationCount").innerHTML = notificationsCount;

    var question = JSON.parse(message);
    var date = new Date().toLocaleString();
    console.log(document.getElementById("signalRNotifications"));

    document.getElementById("signalRNotifications").innerHTML +=
      
        `<div class="list-group-item">
        <div class="row align-items-center">
            <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
            <div class="col text-truncate">
                <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
                <div class="d-block text-muted text-truncate mt-n1">
                  ${date}
                </div>
            </div>
        </div>
    </div>`;

    document.getElementById("signalRNotifications").style.flexDirection ="column-reverse";

 //   var list = document.getElementById("signalRNotifications");
 //   list.insertBefore(document.querySelectorAll(".list-group-item")[0], `<div class="list-group-item">
 //       <div class="row align-items-center">
 //           <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
 //          <div class="col text-truncate">
 //              <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
 //               <div class="d-block text-muted text-truncate mt-n1">
 //                  ${date}
 //              </div>
 //         </div>
 //    </div>
 //</div>`);
    





});

