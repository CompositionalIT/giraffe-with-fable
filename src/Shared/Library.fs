namespace Shared

/// Shared logic between client + server
module Say =
    let hello name = $"{System.DateTime.Now} Hello %s{name}"