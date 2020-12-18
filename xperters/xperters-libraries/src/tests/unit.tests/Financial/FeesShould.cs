using System.Linq;
using xperters.entities;
using Xunit;

public class FeesStructureShould{

    [Fact]
    public void SelectTheRightBand1(){

        const decimal value = 1m;
        var items = MasterDataFactory.GetFeeStructureData().ToList();

        var bands = items.OrderBy(o => o.BandStart);
        var bandLowerValues = bands
                                .Select(x => x.BandStart)
                                .ToArray();

        int result = value.FindFeeBand(bandLowerValues);
        var selectedFeeBand = items.ElementAt(result);
        Assert.Equal(0, result);
        Assert.Equal(500, selectedFeeBand.BandEnd);

    }

    [Fact]
    public void SelectTheRightBand501(){

        const decimal value = 501m;
        var items = MasterDataFactory.GetFeeStructureData().ToList();

        var bands = items.OrderBy(o => o.BandStart);
        var bandLowerValues = bands
                                .Select(x => x.BandStart)
                                .ToArray();

        int result = value.FindFeeBand(bandLowerValues);
        var selectedFeeBand = items.ElementAt(result);
        Assert.Equal(1, result);
        Assert.Equal(1000, selectedFeeBand.BandEnd);

    }    

    [Fact]
    public void SelectTheRightBand1001(){

        const decimal value = 1001m;
        var items = MasterDataFactory.GetFeeStructureData().ToList();

        var bands = items.OrderBy(o => o.BandStart);
        var bandLowerValues = bands
                                .Select(x => x.BandStart)
                                .ToArray();

        int result = value.FindFeeBand(bandLowerValues);
        var selectedFeeBand = items.ElementAt(result);
        Assert.Equal(2, result);
        Assert.Equal(10000, selectedFeeBand.BandEnd);

    }       
}