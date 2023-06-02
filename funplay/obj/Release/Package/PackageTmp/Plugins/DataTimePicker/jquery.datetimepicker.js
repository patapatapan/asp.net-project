//https://xdsoft.net/jqplugins/datetimepicker/
jQuery.datetimepicker.setLocale('zh-TW');
jQuery(document).ready(function () {
    'use strict';
    jQuery('.datetimepicker, #search-from-date, #search-to-date').datetimepicker({
        showSecond: true,
        defaultDate: new Date(),
        dateFormat: 'YYYY/MM/DD',
        timeFormat: 'HH:mm:ss',
        stepHour: 2,
        stepMinute: 10,
        stepSecond: 10
    });
    jQuery('.timepicker').datetimepicker({
        datepicker: false,
        timepicker: true,
        timeFormat: 'HH:mm:ss'
    });
});