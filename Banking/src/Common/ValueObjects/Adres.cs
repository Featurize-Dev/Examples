using Featurize.ValueObjects;

namespace Common.ValueObjects;

public record Adres(string Straat, string Huisnummer, string PostCode, string Plaats, Country Land);