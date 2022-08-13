



    var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
            connection.start().then(() => { //alert("connected")
    }
    );
    connection.on("ReciveQuestions", function (message) {
                var notificationsCount = document.getElementById("notificationCount").innerHTML;
    notificationsCount = parseInt(notificationsCount) + 1;
    document.getElementById("notificationCount").innerHTML = notificationsCount;




    var question = JSON.parse(message);
    console.log(document.getElementById("item"));
    /* console.log(document.querySelectorAll(".loadnotifications")[0]);*/
/*    var list = document.getElementById("item");*/



    //    document.getElementById("item").innerHTML +=
    //        ` <div  class="list-group-item">
    //    <div class="row align-items-center">
    //        <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
    //        <div class="col text-truncate">
    //            <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
    //            <div class="d-block text-muted text-truncate mt-n1">
    //              ${question.SubmitTime}
    //            </div>
    //        </div>
    //    </div>
    //</div>`;


    //list.insertBefore(document.querySelectorAll(".list-group-item")[0], `<div class="list-group-item">
    //    <div class="row align-items-center">
    //        <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
    //        <div class="col text-truncate">
    //            <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
    //            <div class="d-block text-muted text-truncate mt-n1">
    //                ${question.SubmitTime}
    //            </div>
    //        </div>
    //    </div>
    //</div>`);

            });

















//var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
//connection.start().then(() => { //alert("connected")
//}
//);
//connection.on("ReciveQuestions", function (message) {
//    var notificationsCount = document.getElementById("notificationCount").innerHTML;
//    notificationsCount = parseInt(notificationsCount) + 1;
//    document.getElementById("notificationCount").innerHTML = notificationsCount;
    
    

   
//    var question = JSON.parse(message);
//    console.log(document.getElementById("item"));
//    console.log(document.querySelectorAll(".loadnotifications")[0]);
//    var list = document.getElementById("item");
//    list.insertBefore(document.querySelectorAll(".loadnotifications")[0], `<div  class="list-group-item loadnotifications">
//        <div class="row align-items-center">
//            <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
//            <div class="col text-truncate">
//                <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
//                <div class="d-block text-muted text-truncate mt-n1">
//                ${question.SubmitTime}
//                </div>
//            </div>
//       </div>
//    </div>`);

//});
    //console.log(question);
    //console.log(question.ID);
    //console.log(parseInt(notificationsCount))
    //console.log(document.getElementById("item").innerHTML)
    //if (parseInt(notificationsCount) === 0) {
    //    document.getElementById("viewall").style.visibility = "hidden";
    //}
    
  
            


    //}
    //else {
    //    console.log(document.getElementById("item").innerHTML)
    //    document.getElementById("item").innerHTML=
    //        ` <div  class="list-group-item loadnotifications">
    //    <div class="row align-items-center">
    //        <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
    //        <div class="col text-truncate">
    //            <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
    //            <div class="d-block text-muted text-truncate mt-n1">
    //              ${question.SubmitTime}
    //            </div>
    //        </div>
    //    </div>
    //</div>`;

    //}

 
    //document.getElementById("item").innerHTML +=
    //    ` <div  class="list-group-item">
    //    <div class="row align-items-center">
    //        <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
    //        <div class="col text-truncate">
    //            <a href="/Question/AnswerQuestion?questionId=${question.ID}" class="text-body d-block">${question.User.UserName}</a>
    //            <div class="d-block text-muted text-truncate mt-n1">
    //              ${question.SubmitTime}
    //            </div>
    //        </div>
    //    </div>
    //</div>`;








