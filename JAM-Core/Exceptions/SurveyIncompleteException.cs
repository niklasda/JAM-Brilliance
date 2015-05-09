using System;
using System.Diagnostics.CodeAnalysis;

namespace JAM.Core.Exceptions
{
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "No need, nothing added")]
    public class SurveyIncompleteException : Exception
    {
    }
}