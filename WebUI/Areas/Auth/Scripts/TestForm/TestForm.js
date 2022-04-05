$(document).ready(function () {
    ko.validation.init({
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block',
        decorateInputElement: true
    });    

    var TestFrmVM = function () {
        var self = this;

        $("#localPurchase").hide();
        $("#btnCancel").hide();

        self.Id = ko.observable();

        self.FinalForm = ko.observable();
        
        

        self.loadPurchaseOrder = function () {
            //self.PurchaseOrderDetail([]);
            //if (self.Id() > 0) {
                return $.ajax({
                    type: "GET",
                    url: "/Auth/CreateForm/CreateFormByMenuid?menuId=265",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                       
                        self.FinalForm(data);
                        
                    },
                    error: function (error) {
                        alert(error.status + "<--and--> " + error.statusText);
                    }
                });
            //}
        }        

        self.queryString = function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        };
    }

    var vm = new TestFrmVM();
    vm.Id(vm.queryString("menuId"));
    
    vm.loadPurchaseOrder();

    ko.applyBindings(vm, document.getElementById("TestFrmGen")[0]);

});




