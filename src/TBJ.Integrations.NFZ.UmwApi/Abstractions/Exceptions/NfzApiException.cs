namespace TBJ.Integrations.NFZ.UmwApi.Abstractions.Exceptions;

/// <summary>
/// Wyjątek rzucany gdy API Umowy NFZ zwróci błąd HTTP lub odpowiedź z sekcją <c>errors</c>.
/// </summary>
public sealed class NfzApiException : Exception
{
    /// <summary>
    /// Kod statusu HTTP odpowiedzi (lub <c>null</c> gdy błąd wystąpił przed wysłaniem żądania).
    /// </summary>
    public int? HttpStatusCode { get; }

    /// <summary>
    /// Błędy zwrócone przez API w sekcji <c>errors</c>. Może być puste jeśli błąd HTTP nie zawierał treści JSON.
    /// </summary>
    public IReadOnlyList<Models.Common.ApiError> Errors { get; }

    /// <summary>
    /// Tworzy wyjątek z komunikatem i listą błędów API.
    /// </summary>
    /// <param name="message">Komunikat błędu.</param>
    /// <param name="httpStatusCode">Kod statusu HTTP.</param>
    /// <param name="errors">Lista błędów API.</param>
    public NfzApiException(string message, int? httpStatusCode = null, IReadOnlyList<Models.Common.ApiError>? errors = null)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
        Errors = errors ?? [];
    }

    /// <summary>
    /// Tworzy wyjątek opakowujący wewnętrzny wyjątek (np. błąd sieci lub timeout).
    /// </summary>
    /// <param name="message">Komunikat błędu.</param>
    /// <param name="innerException">Wewnętrzny wyjątek.</param>
    public NfzApiException(string message, Exception innerException)
        : base(message, innerException)
    {
        HttpStatusCode = null;
        Errors = [];
    }
}
