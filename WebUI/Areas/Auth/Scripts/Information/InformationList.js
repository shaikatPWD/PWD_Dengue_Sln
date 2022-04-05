$(document).ready(function () {
    function infomationListVm() {
        var self = this;

        //Counting Ovservations
        self.pendingObs = ko.observable();
        self.inProgressObs = ko.observable();
        self.completedObs = ko.observable();

        self.Id = ko.observable();
        self.LoadData = ko.observableArray(informationInfo);
        self.Url = ko.observable(); //url
        self.Link1 = ko.observable();
        self.Title1 = ko.observable('PDF');
        //debugger;
        self.GetTotalPendingObs = function () {
            
            return $.ajax({
                type: "GET",
                url: '/Auth/Information/GetPendingObs',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.pendingObs(data); //Put the response in ObservableArray
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.GetTotalInProgressObs = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Information/GetInProgressObs',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.inProgressObs(data); //Put the response in ObservableArray
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.GetTotalCompletedObs = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Information/GetCompletedObs',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.completedObs(data); //Put the response in ObservableArray
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
        
        self.queryString = function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    }
    var aabmvm = new infomationListVm();
    //cardVm.Id(cardVm.queryString("id"));
    aabmvm.GetTotalPendingObs();
    aabmvm.GetTotalInProgressObs();
    aabmvm.GetTotalCompletedObs();
    ko.applyBindings(aabmvm, document.getElementById("InfoDtlVW"));
});