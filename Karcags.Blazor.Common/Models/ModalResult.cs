using System;

namespace Karcags.Blazor.Common.Models
{
    public class ModalResult
    {
        public object Data { get; }
        public Type DataType { get; }
        public bool Cancelled { get; }
        public Type ModalType { get; set; }

        protected ModalResult(object data, Type resultType, bool cancelled, Type modalType)
        {
            this.Data = data;
            this.DataType = resultType;
            this.Cancelled = cancelled;
            this.ModalType = modalType;
        }

        public static ModalResult Ok<T>(T result)
        {
            return Ok(result, default);
        }

        public static ModalResult Ok<T>(T result, Type modalType)
        {
            return new ModalResult(result, typeof(T), false, modalType);
        }

        public static ModalResult Cancel()
        {
            return Cancel(default);
        }

        public static ModalResult Cancel(Type modalType)
        {
            return new ModalResult(default, typeof(object), true, modalType);
        }
    }
}