using Nancy;
namespace ProjectCore
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {return View["index.cshtml"];};
    }
  }
}
