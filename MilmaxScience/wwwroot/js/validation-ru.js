jQuery.extend(jQuery.validator.messages, {
    required: "Обязательное поле",
    remote: "Исправьте это поле",
    email: "Введите корректный Email",
    url: "Введите корректный URL",
    date: "Введите корректную дату",
    dateISO: "Введите корректную дату",
    number: "Введите число",
    digits: "Допустимы только цифры",
    equalTo: "Значения не совпадают",
    maxlength: jQuery.validator.format("Не более {0} символов"),
    minlength: jQuery.validator.format("Не менее {0} символов"),
    rangelength: jQuery.validator.format("Введите от {0} до {1} символов"),
    range: jQuery.validator.format("Введите значение от {0} до {1}"),
    max: jQuery.validator.format("Введите значение меньше либо равное {0}"),
    min: jQuery.validator.format("Введите значение больше либо равное {0}")
});