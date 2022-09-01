using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WRP3.Infrastructure.GoogleRecaptcha.Models;

namespace WRP3.Infrastructure.GoogleRecaptcha.IServices
{
    public interface IGoogleRecaptchaService
    {
        Task<GoogleReCaptcha> SiteVerify(GoogleReCaptcha googleReCaptcha);
    }
}
