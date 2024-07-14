using Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypotheken.Domain.HypotheekGevers;
public class Rechtspersoon : Hypotheekgever
{
    public override Amount JaarInkomen { get; } = 0;
}
