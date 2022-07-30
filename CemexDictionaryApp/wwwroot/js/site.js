



var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
connection.start().then(() => { //alert("connected")
}
);
connection.on("ReciveQuestions", function (message) {
    var notificationsCount = document.getElementById("notificationCount").innerHTML;
    notificationsCount = parseInt(notificationsCount) + 1;
    document.getElementById("notificationCount").innerHTML = notificationsCount;
    
    

   
    var question = JSON.parse(message);
    console.log(question);

   
    document.getElementById("item").innerHTML +=
        ` <div class="list-group-item">
        <div class="row align-items-center">
            <div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
            <div class="col text-truncate">
                <a href="#" class="text-body d-block">${question.UserId}</a>
                <div class="d-block text-muted text-truncate mt-n1">
                  ${question.SubmitTime}
                </div>
            </div>
         
        </div>
    </div>`;




    //a.href = "/Question/GetQuestion/" + m.Id;
    //var header = document.getElementById("box");

    //header.insertBefore(div, header.firstElementChild.nextSibling);
  
});
