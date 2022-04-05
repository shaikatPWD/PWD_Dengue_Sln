$(document).ready(function () {
    function populateSubmodules(isEdit) {

        var moduleCombo = $("#tr_ModuleId select");
        var submoduleCombo = $("#tr_SubModuleId select");
        $(moduleCombo).attr("id", "ModuleId").attr("name", "ModuleId");
        $(submoduleCombo).attr("id", "SubModuleId").attr("name", "SubModuleId");

        var selectedModuleId = $("#jqGrid").jqGrid('getRowData', $("#jqGrid")[0].p.selrow).ModuleId | 0;
        $(moduleCombo)
            .html("<option value=''>Loading Modules...</option>")
            .attr("disabled", "disabled");
        $.ajax({
            url: '/Submodule/GetCorrespondingModules',
            type: "GET",
            success: function (moduleHtml) {
                $(moduleCombo).removeAttr("disabled").html(moduleHtml);

                if (isEdit) {
                    $(moduleCombo).val(selectedModuleId);
                } else {
                    $(moduleCombo).selectedIndex = 0;
                }
                updateSubmoduleCallBack(isEdit, $(moduleCombo).val(), submoduleCombo);
            }
        });
        $(moduleCombo).bind("change", function (e) {
            updateSubmoduleCallBack(false, $(moduleCombo).val(), submoduleCombo);
        });
    }
    function updateSubmoduleCallBack(isEdit, selectedModuleId, submoduleCombo) {
        var url = '/Menu/GetCorrespondingSubModules/?moduleId=' + selectedModuleId;
        $(submoduleCombo)
            .html("<option value=''>Loading submodules...</option>")
            .attr("disabled", "disabled");
        $.ajax({
            url: url,
            type: "GET",
            success: function (submodJson) {
                var submods = eval(submodJson);
                var subModuleHtml = "";
                $(submods).each(function (i, option) {
                    subModuleHtml += '<option value="' + option.Id + '">' + option.DisplayName + '</option>';
                });
                $(submoduleCombo).removeAttr("disabled").html(subModuleHtml);
                if (isEdit) {
                    var selectedSubmoduleId = $("#jqGrid").jqGrid('getRowData', $("#jqGrid")[0].p.selrow).SubModuleId | 0;
                    $(submoduleCombo).val(selectedSubmoduleId);
                } else {
                    $(submoduleCombo).selectedIndex = 0;
                }
                $(submoduleCombo).focus();
            }
        });
    }
    $(function () {
        $(window).on('resize.jqGrid', function () {
            $("#jqGrid").jqGrid('setGridWidth', $(".page-content").width());
        });
        //resize on sidebar collapse/expand
        var parent_column = $("#jqGrid").closest('[class*="col-"]');
        $(document).on('settings.ace.jqGrid', function (ev, event_name, collapsed) {
            if (event_name === 'sidebar_collapsed' || event_name === 'main_container_fixed') {
                //setTimeout is for webkit only to give time for DOM changes and then redraw!!!
                setTimeout(function () {
                    $("#jqGrid").jqGrid('setGridWidth', parent_column.width());
                }, 0);
            }
        });
        $("#jqGrid").jqGrid({
            url: "/Menu/GetMenus",
            datatype: 'json',
            mtype: 'Get',
            colNames: ['Id', 'Name', 'Display Name', 'Description', 'Sl', 'Url', 'Module Id', 'Module Name', 'SubModule Id', 'SubModule Name'],
            colModel: [
                { key: true, hidden: true, name: 'Id', index: 'Id', editable: false },
                { key: false, name: 'Name', index: 'Name', editable: true, editrules: { custom_func: validateText, custom: true, required: true }, searchoptions: { sopt: ['eq', 'ne', 'cn'] }, classes: "grid-col" },
                { key: false, name: 'DisplayName', index: 'DisplayName', editable: true, editrules: { custom_func: validateText, custom: true, required: true }, formoptions: { label: "DisplayName" }, searchoptions: { sopt: ['eq', 'ne', 'cn'] }, classes: "grid-col" },
                { key: false, name: 'Description', index: 'Description', editable: true, editrules: { custom_func: validateText, custom: true, required: true }, searchoptions: { sopt: ['eq', 'ne', 'cn'] }, classes: "grid-col" },
                { key: false, name: 'Sl', index: 'Sl', editable: true, editrules: { custom_func: validatePositive, custom: true, required: true }, align: 'right', searchoptions: { sopt: ['eq', 'ne'] }, classes: "grid-col" },
                { key: false, name: 'Url', index: 'Url', editable: true, editrules: { required: true }, searchoptions: { sopt: ['eq', 'ne', 'cn'] }, classes: "grid-col" },
                { key: false, hidden: true, name: 'ModuleId', index: 'ModuleId', editable: true, edittype: "select", editrules: { edithidden: true, required: true }, formoptions: { label: "Module: " } },
                { key: false, name: 'ModuleName', index: 'ModuleName', editable: false, label: "Module Name", searchoptions: { sopt: ['eq', 'ne', 'cn'] }, classes: "grid-col" },
                { key: false, hidden: true, name: 'SubModuleId', index: 'SubModuleId', editable: true, edittype: "select", editrules: { edithidden: true, required: true }, formoptions: { label: "Submodule: " } },
                { key: false, name: 'SubModuleName', index: 'SubModuleName', label: "Submodule Name", editable: false, searchoptions: { sopt: ['eq', 'ne', 'cn'] }, classes: "grid-col" }
            ],
            ondblClickRow: function (rowid) {
                jQuery("#jqGrid").jqGrid('editGridRow', rowid);
            },
            loadonce: true,
            pager: jQuery('#jqGridPager'),
            altRows: true,
            viewrecords: true,
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
            hoverrows: true,
            sortable: true,
            //width: '70%',
            caption: 'Menu Records',
            emptyrecords: 'No Module Records are Available to Display',
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: true,
            height: 'auto',//set auto height
            multiselect: false,
            loadComplete: function () {
                var table = this;
                setTimeout(function () {
                    styleCheckbox(table);

                    updateActionIcons(table);
                    updatePagerIcons(table);
                    enableTooltips(table);
                }, 0);
            }
        });
        $(window).triggerHandler('resize.jqGrid');//trigger window resize to make the grid get the correct size
        jQuery("#jqGrid").jqGrid('navGrid', '#jqGridPager',
            //{ edit: true, add: true, del: true, search: true, refresh: true },
            {
                edit: true,
                editicon: 'ace-icon fa fa-pencil blue',
                add: true,
                addicon: 'ace-icon fa fa-plus-circle purple',
                del: true,
                delicon: 'ace-icon fa fa-trash-o red',
                search: true,
                searchicon: 'ace-icon fa fa-search orange',
                refresh: true,
                refreshicon: 'ace-icon fa fa-refresh green',
                view: true,
                viewicon: 'ace-icon fa fa-search-plus grey'
            },            
            {
                zIndex: 100,
                url: '/Menu/SaveMenu',
                closeOnEscape: true,
                closeAfterEdit: true,
                width: 'auto',
                height: 'auto',
                viewPagerButtons: false,
                recreateForm: true,
                onInitializeForm: function (formId) { populateSubmodules(true); },
                afterComplete: function (response) {
                    if (response.responseText) {
                        Messager.ShowMessage(response.responseText);
                    }
                }
            },
            {
                zIndex: 100,
                url: '/Menu/SaveMenu',
                closeOnEscape: true,
                width: 'auto',
                height: 'auto',
                closeAfterAdd: true,
                onInitializeForm: function (formId) { populateSubmodules(false); },
                afterComplete: function (response) {
                    if (response.responseText) {
                        Messager.ShowMessage(response.responseText);
                    }
                }
            },
            {
                zIndex: 100,
                url: "/Menu/DeleteMenu",
                closeOnEscape: true,
                closeAfterDelete: true,
                recreateForm: true,
                beforeShowForm: function (e) {
                    var form = $(e[0]);
                    if (form.data('styled')) return false;

                    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                    style_delete_form(form);

                    form.data('styled', true);
                },
                onClick: function (e) {
                    //alert(1);
                },
                msg: "Are you sure to delete this Submodule? ",
                afterComplete: function (response) {
                    if (response.responseText) {
                        Messager.ShowMessage(response.responseText);
                    }
                }
            },
            {
                //search form
                closeAfterSearch:true,
                recreateForm: true,
                afterShowSearch: function (e) {
                    var form = $(e[0]);
                    form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />');
                    style_search_form(form);
                },
                afterRedraw: function () {
                    style_search_filters($(this));
                }
                ,
                multipleSearch: true
                /**
                multipleGroup:true,
                showQuery: true
                */
            },
            {
                //view record form
                recreateForm: true,
                beforeShowForm: function (e) {
                    var form = $(e[0]);
                    form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />');
                }
            },
            {
                closeOnEscape: true,
                multipleSearch: true,
                closeAfterSearch: true
            }
        );
        function style_edit_form(form) {
            //enable datepicker on "sdate" field and switches for "stock" field
            form.find('input[name=sdate]').datepicker({ format: 'yyyy-mm-dd', autoclose: true })

            form.find('input[name=stock]').addClass('ace ace-switch ace-switch-5').after('<span class="lbl"></span>');
            //don't wrap inside a label element, the checkbox value won't be submitted (POST'ed)
            //.addClass('ace ace-switch ace-switch-5').wrap('<label class="inline" />').after('<span class="lbl"></span>');


            //update buttons classes
            var buttons = form.next().find('.EditButton .fm-button');
            buttons.addClass('btn btn-sm').find('[class*="-icon"]').hide();//ui-icon, s-icon
            buttons.eq(0).addClass('btn-primary').prepend('<i class="ace-icon fa fa-check"></i>');
            buttons.eq(1).prepend('<i class="ace-icon fa fa-times"></i>')

            buttons = form.next().find('.navButton a');
            buttons.find('.ui-icon').hide();
            buttons.eq(0).append('<i class="ace-icon fa fa-chevron-left"></i>');
            buttons.eq(1).append('<i class="ace-icon fa fa-chevron-right"></i>');
        }

        function style_delete_form(form) {
            var buttons = form.next().find('.EditButton .fm-button');
            buttons.addClass('btn btn-sm btn-white btn-round').find('[class*="-icon"]').hide();//ui-icon, s-icon
            buttons.eq(0).addClass('btn-danger').prepend('<i class="ace-icon fa fa-trash-o"></i>');
            buttons.eq(1).addClass('btn-default').prepend('<i class="ace-icon fa fa-times"></i>')
        }

        function style_search_filters(form) {
            form.find('.delete-rule').val('X');
            form.find('.add-rule').addClass('btn btn-xs btn-primary');
            form.find('.add-group').addClass('btn btn-xs btn-success');
            form.find('.delete-group').addClass('btn btn-xs btn-danger');
        }
        function style_search_form(form) {
            var dialog = form.closest('.ui-jqdialog');
            var buttons = dialog.find('.EditTable')
            buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'ace-icon fa fa-retweet');
            buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'ace-icon fa fa-comment-o');
            buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'ace-icon fa fa-search');
        }

        function beforeDeleteCallback(e) {
            var form = $(e[0]);
            if (form.data('styled')) return false;

            form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
            style_delete_form(form);

            form.data('styled', true);
        }

        function beforeEditCallback(e) {
            var form = $(e[0]);
            form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
            style_edit_form(form);
        }
        //it causes some flicker when reloading or navigating grid
        //it may be possible to have some custom formatter to do this as the grid is being created to prevent this
        //or go back to default browser checkbox styles for the grid
        function styleCheckbox(table) {
            /**
                $(table).find('input:checkbox').addClass('ace')
                .wrap('<label />')
                .after('<span class="lbl align-top" />')
    	
    	
                $('.ui-jqgrid-labels th[id*="_cb"]:first-child')
                .find('input.cbox[type=checkbox]').addClass('ace')
                .wrap('<label />').after('<span class="lbl align-top" />');
            */
        }

        //unlike navButtons icons, action icons in rows seem to be hard-coded
        //you can change them like this in here if you want
        function updateActionIcons(table) {
            /**
            var replacement = 
            {
                'ui-ace-icon fa fa-pencil' : 'ace-icon fa fa-pencil blue',
                'ui-ace-icon fa fa-trash-o' : 'ace-icon fa fa-trash-o red',
                'ui-icon-disk' : 'ace-icon fa fa-check green',
                'ui-icon-cancel' : 'ace-icon fa fa-times red'
            };
            $(table).find('.ui-pg-div span.ui-icon').each(function(){
                var icon = $(this);
                var $class = $.trim(icon.attr('class').replace('ui-icon', ''));
                if($class in replacement) icon.attr('class', 'ui-icon '+replacement[$class]);
            })
            */
        }
        //replace icons with FontAwesome icons like above
        function updatePagerIcons(table) {
            var replacement =
            {
                'ui-icon-seek-first': 'ace-icon fa fa-angle-double-left bigger-140',
                'ui-icon-seek-prev': 'ace-icon fa fa-angle-left bigger-140',
                'ui-icon-seek-next': 'ace-icon fa fa-angle-right bigger-140',
                'ui-icon-seek-end': 'ace-icon fa fa-angle-double-right bigger-140'
            };
            $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
                var icon = $(this);
                var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

                if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
            })
        }

        function enableTooltips(table) {
            $('.navtable .ui-pg-button').tooltip({ container: 'body' });
            $(table).find('.ui-pg-div').tooltip({ container: 'body' });
        }

        //var selr = jQuery(grid_selector).jqGrid('getGridParam','selrow');

        $(document).one('ajaxloadstart.page', function (e) {
            $(grid_selector).jqGrid('GridUnload');
            $('.ui-jqdialog').remove();
        });
        $('#jqGrid').setGroupHeaders(
            {
                useColSpanStyle: true,
                groupHeaders: [
                    { "numberOfColumns": 4, "titleText": "General Info", "startColumnName": 'Name' },
                    { "numberOfColumns": 5, "titleText": "Secondary Details", "startColumnName": 'Url' }]

            });
    });
});
