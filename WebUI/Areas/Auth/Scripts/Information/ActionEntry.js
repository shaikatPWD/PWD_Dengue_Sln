$(document).ready(function () {
    var IfoActionViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.FullName = ko.observable();
        self.Mobile = ko.observable();
        self.ComplainID = ko.observable();
        self.DhakaInOut = ko.observable();
        self.AreaID = ko.observable();
        self.AreaName = ko.observable();
        self.DistrictID = ko.observable();
        self.DistrictName = ko.observable();
        self.Location = ko.observable();
        self.Remarks = ko.observable();

        self.CombindArea = ko.observable();

        self.Image1 = ko.observable();
        self.Image2 = ko.observable();
        self.Image3 = ko.observable();
        self.Image4 = ko.observable();
        self.Image5 = ko.observable();

        self.ComplainStatus = ko.observable('');// Blocked or Not used
        self.StatusId = ko.observable();
        self.StatusName = ko.observable('');
        self.StatusList = ko.observableArray([]);

        self.LoadInfoStatus = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Information/GetStatus',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.StatusList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadInformation = function () {
            if (self.Id() > 0) {
                return $.ajax({
                    type: "GET",
                    url: "/Auth/Information/LoadInformation?id=" + self.Id(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //data.DefImage("/UploadImages/ComplainImages/noimage.png");
                        //console.log(data.Image1);
                        self.Id(data.Id);
                        self.FullName(data.FullName);
                        self.Mobile(data.Mobile);
                        self.ComplainID(data.ComplainID);
                        self.DhakaInOut(data.DhakaInOut);
                        self.AreaID(data.AreaID);
                        self.AreaName(data.AreaName);
                        self.DistrictID(data.DistrictID);
                        self.DistrictName(data.DistrictName);
                        if (data.AreaID > 0) {
                            self.CombindArea(data.AreaName);
                        }
                        else {
                            self.CombindArea(data.DistrictName);
                        }
                        self.Location(data.Location);
                        self.Remarks(data.Remarks);
                        $.when(self.LoadInfoStatus()).done(function () {
                            self.ComplainStatus(data.ComplainStatus);
                            self.StatusName(data.StatusName);
                        });
                        if (data.Image1 != null && data.Image1 != "") {
                            self.Image1(data.Image1);
                        }
                        else {
                            self.Image1("/UploadImages/ComplainImages/noimage.jpg");
                        }
                        if (data.Image2 != null && data.Image2 != "") {
                            self.Image2(data.Image2);
                        }
                        else {
                            self.Image2("/UploadImages/ComplainImages/noimage.jpg");
                        }
                        if (data.Image3 != null && data.Image3 != "") {
                            self.Image3(data.Image3);
                        }
                        else {
                            self.Image3("/UploadImages/ComplainImages/noimage.jpg");
                        }
                        if (data.Image4 != null && data.Image4 != "") {
                            self.Image4(data.Image4);
                        }
                        else {
                            self.Image4("/UploadImages/ComplainImages/noimage.jpg");
                        }
                        if (data.Image5 != null && data.Image5 != "") {
                            self.Image5(data.Image5);
                        }
                        else {
                            self.Image5("/UploadImages/ComplainImages/noimage.jpg");
                        }
                    },
                    error: function (error) {
                        alert(error.status + "<--and--> " + error.statusText);
                    }
                });
            }
            else {
                self.Initializer();
            }
        }

        self.Submit = function () {

            var submitJobCardData = {
                Id: self.Id(),
                FullName: self.FullName(),
                Mobile: self.Mobile(),
                ComplainID: self.ComplainID(),
                DhakaInOut: self.DhakaInOut(),
                AreaID: self.AreaID(),
                AreaName: self.AreaName(),
                DistrictID: self.DistrictID(),
                DistrictName: self.DistrictName(),
                Location: self.Location(),
                Remarks: self.Remarks(),
                ComplainStatus: self.ComplainStatus(),
                Image1: self.Image1(),
                Image2: self.Image2(),
                Image3: self.Image3(),
                Image4: self.Image4(),
                Image5: self.Image5(),

            };
            //if (self.IsValid()) {
            $.ajax({
                url: '/Auth/Information/SaveUpdateActions',
                type: 'POST',
                contentType: 'application/json',
                data: ko.toJSON(submitJobCardData),
                success: function (data) {
                    $('#successModal').modal('show');
                    $('#successModalText').text(data.Message);
                    if (data.Id > 0) {
                        //console.log(data.Id);
                        self.LoadInformation();
                        //$('#draftBtn').prop('disabled', true);
                        //$('#forwardBtn').prop('disabled', true);
                        //$('#cancelBtn').prop('disabled', true);
                    }
                    //self.Id(data.Id);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
            //} else {
            //    $('#successModal').modal('show');
            //    $('#successModalText').text("Please Insert Reference, Customer Name, Customer Type, Customer Phone, Vehicle Reg. No and Foreman");
            //    self.errors.showAllMessages();
            //};
        };

        self.Initializer = function () {
            self.LoadInfoStatus();
        };

        self.queryString = function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        };
    };

    var infoVm = new IfoActionViewModel();

    infoVm.Id(infoVm.queryString("id"));
    infoVm.LoadInformation();

    ko.applyBindings(infoVm, document.getElementById("actionEntry")[0]);
})