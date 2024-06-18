using Featurize.ValueObjects;

namespace Hypotheken.Domain;

public record Adres(string Straat, string Huisnummer, string PostCode, string Plaats, Country Land);