//ko.bindingHandlers.datePicker = {
//    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
//        // Register change callbacks to update the model
//        // if the control changes.       
//        ko.utils.registerEventHandler(element, "change", function () {
//            var value = valueAccessor();
//            value(new Date(element.value));
//        });
//    },
//    // Update the control whenever the view model changes
//    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
//        var value = valueAccessor();
//        element.value = value().toISOString();
//    }
//};

//var viewModel = {
//    MyDate: ko.observable(new Date()),
//    log: ko.observable(""),
//    logDate: function () {
//        this.log(this.log() + this.MyDate() + " : " +
//            typeof (this.MyDate()) + "<br>");
//    }
//};

//viewModel.MyDate.subscribe(function (date) {
//    viewModel.logDate();
//});

//ko.applyBindings(viewModel);

//viewModel.logDate()


ko.bindingHandlers.enableDisable = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        ko.bindingHandlers.enableDisable.update(element, valueAccessor);
    },
    update: function (element, valueAccessor) {
        var enabledDates = valueAccessor()();
        //apply disabled dates
        $(element).data("DateTimePicker").enabledDates(enabledDates);
    }
}

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        //initialize datepicker with some optional options
        var options = {
            format: 'DD/MM/YYYY HH:mm',
            defaultDate: valueAccessor()()
        };

        if (allBindingsAccessor() !== undefined) {
            if (allBindingsAccessor().datepickerOptions !== undefined) {
                options.format = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : options.format;
            }
        }

        $(element).datetimepicker(options);
        var picker = $(element).data('datetimepicker');

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                value(event.date);
            }
        });

        var defaultVal = $(element).val();
        var value = valueAccessor();
        value(moment(defaultVal, options.format));
    },
    update: function (element, valueAccessor) {
        var widget = $(element).data("datepicker");
        //when the view model is updated, update the widget
        if (widget) {
            widget.date = ko.utils.unwrapObservable(valueAccessor());
            if (widget.date) {
                widget.setValue();
            }
        }
    }
};


function viewModel() {
    var self = this;

    self.Countries = ko.observableArray(['France', 'Germany', 'Spain']);
    self.SelecteItem = ko.observable();
    self.EnabledDates = ko.observableArray();
    self.SelectedDate = ko.observable(new Date());

    self.SelecteItem.subscribe(function () {
        var tempArray = [];
        if (self.SelecteItem() == "France") {

            tempArray.push(new moment('Date(1431514972533)'));
            tempArray.push(new moment('Date(1431082972533)'));

        } else {
            tempArray.push(new moment(new Date()));
        }
        self.EnabledDates(tempArray);

    });
}

var testviewModel = new viewModel();

ko.applyBindings(testviewModel);