$(document).ready(function () {   

    var PeriodListViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.OfficeList = ko.observableArray([]);

        self.Url = ko.observable();

        self.LoadPeriods = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Workrecord/GetAllWorksOffices',
                contentType: "application/json; charset=utf-8",
                dataType: "json",                
                success: function (data) {
                    self.OfficeList(data);
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
        };
    };

    var infoVm = new PeriodListViewModel();
    infoVm.LoadPeriods();
    ko.applyBindings(infoVm, document.getElementById("mmInfoEntry")[0]);
})