namespace App.Entity.Enum
{
    public enum OrderDirection
    {
        Asc = 1,
        Desc
    }

    public enum DataTypes
    {
        Int32 = 1,
        String,
        Boolean,
        DateTime,
        Int64,
        Byte,
        Double,
        Char,
        Decimal,
        SByte,
        UInt32,
        Int16,
        UInt16,
        UInt64,
        Single,
        Object,
    }

    public enum InputTypes
    {
        text = 1,
        number,
        email,
        hidden,
        button,
        checkbox,
        file,
        color,
        date,
        time,
        image,
        password,
        radio,
        range,
        reset,
        search,
        submit,
        tel,
        url,
        week
    }

    public enum FormTags
    {
        input = 1,
        label,
        button,
        select,
        h4,
        option,
        optgroup,
        textarea,
        datalist,
        fieldset,
        form,
        legend,
        meter,
        output,
        progress
    }

    public enum SummaryType
    {
        Room = 1,
        Facilities,
        Distance,
        Rules
    }

    public enum ViewType
    {
        SeaView = 1,
        Mountain,
        Lake,
        City,

    }
}
