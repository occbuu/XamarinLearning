﻿using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Demo.Controllers
{
    using Models;
    using Utility;

    /// <summary>
    /// File controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/File")]
    public class FileController : BaseController
    {
        #region -- Methods --

        /// <summary>
        /// Upload
        /// POST api/File/Upload
        /// </summary>
        /// <returns>Return the result</returns>
        [Route("Upload")]
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            var msg = new FileModel();

            try
            {
                var req = HttpContext.Current.Request;

                if (req.Files.Count > 0)
                {
                    foreach (string f in req.Files)
                    {
                        var file = req.Files[f];
                        var fileName = file.FileName;
                        fileName = fileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);
                        file.SaveAs(filePath);

                        msg.URL = "/Uploads/" + fileName;
                        msg.DateUpload = DateTime.Now;
                        msg.Success = true;
                    }
                }
                else
                {
                    msg.ErrMsg = "No file";
                }
            }
            catch (Exception ex)
            {
                msg.ErrMsg = ex.Message;
            }

            return Response(msg);
        }

        /*
            {
              "ObjectID": "99151121000001",
              "Image64": "iVBORw0KGgoAAAANSUhEUgAAATgAAAGJCAIAAACRi7jtAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAgAElEQVR42u2dbYwdxZnvj3S/734Jim60V7LyAcmysoHYMmDHJhvkWYiNY4ixlg9jyezGOMgSgxWxwsZeRci2khtywUIbroRnkW54WSAXz4KyMpNFWtmRdbXHq5DYY5jBmPGIEVpnZe9gfzHBc//d/56amn6prn493dVP69HRmTN9zulTXb96Xuqpp3qTk78TERFpuPSkCUREBFQREREBVUREQBURERFQRUREBFQREQFVREREQBURERFQRUQEVJHc8vHRI1P3b5i+Z835h7ZdeOWoNIiIgNpESj/dtE4XvCLNIiKgNks+GLo9BCrko2MvS8uICKhNkfPHj0UppXzYPyHtIyKgNtTuVQJ/VdpHREBthEw/9+MkUMVZFRFQW6BRxQAWEVCbIhdeOWoGdebAHmklAVVkwPLhieNmUEWpCqjSCs2dntHl4uG90koCqsiA5aNHtotSFRFQmy5QmKmgSl6hgCpSky/68dEjYHLmwJ6LT//o/PFjixr12MupoMqcqoAqUnnuUaxxC/YUrtP3rEm3fk8cl8YUUEViZOJMH2pw+vFdHlQPbcMTKENYofbMGDIEKdPP/djS+sVXyx0RUEUixmr/xOSm9UnYTN2/AXTpFmx84tHju9LNWv9DUk/7YOj26DgCkTsloHY7uc+CsYDYpT5n1g+B3ZsJaRWCwhtBL5ze1PFCREB1V5LVaVK8B3ZyyCpOTTwKWIV1nWYk87Qkixr/kikcAbWLAlWZCVSdGX01qc00KZ1VG6VKGmPPxMgilrCAKqZvNpl6cKNa+wJlm3o+MIMJbTmhig+P/S8GBblxAmrngklFQFW4UrvaxHU9bzMNaVq/Bm0v/qqAKktb1uU2hsEPjFtzWi8jyamRJ4wgSRpV92NFBFRhNY9AYXoUGWNUsH5TfVpcksEsB+1y1wTUjmb/gbFyVKs/oZIaOk4F3nA9+AoJKQmoHV7jAlcz44RNPoGRbI4qQWcatK6AKqBKRmE/1c8sxaE1g+olSBhTpuROCagiQQKwIZwzWAHDGE3gx0IzSwqEgCriGcOWyQyDEuhe2skXD+/F4AJ0xSoWULs73Qr15ZUgq8WDLY7u+Ye2gdtMa4BEBFRHWIW12Vhj2ByXWly7J3aygOqqIkUvrzq2VKfErisQEVBbzKdNTQYhVkRArVvOHz/mpRY5zWdslr/UVRNQ26FCvXipxfIXh8Vb2WNRzkJEQB1M5iB6p0suqChYAdW1RPyGT5A2QcGKByugDhLRNs6yDEoEVwFVEG0VrjIHK6DWEC4qaxVbZ2X6njWy57KAWqF4JcU6NuNS6eyrRIYF1PLnRcXWFUtYQG20rStB3aotYW7VISKg5l+bJlOjtU26imoVUPOITeFckXJVqyRICKhi7rbGaxXeBFSrZMBWrOp22wwW5ATUlOiuTMA0ZPJGXFYBNV5Sa+SK1Oyy6ttniQioQTKDsNFAkfCSgCoBXmFVQBVdKiKsCqglroMRDFohskSuu6Di3gsALVqALnHgLoLq1deVmZhWyQdDtwurnQPVsA+3SHOLgD+4UWjsEKiGvX1FJMdQQJUAkogEgQVUO9dUOroDIjvNOQ5q/urYd9/xH+v//A+rvkqZ/dqfxconK7+K03ByR4DBj8VPhiQ1CJsLp5XbLLiPgqWzoObI5lVwev1seMunu3dc2jdy8bmfQq48c/jKi88vCv585jBOwGk4mdC6SuziD8Sfu3d88tQT4dZ48Xm2Ev7FNuG7Pl3+5bKIlZJL7mpU+/Vrd99BPvEcXe3CS6MfvvvO5NTk5ctXZr+4cXV+3iBzc3Of/+636KnooItd0w1E776DehLggcxr47/6r09mzK0BQYu9P3Nx6t9OXXj7TaCL93oNW7hZpu7fIGQ6CKptDEkhunsH+Jw4d3bis6sTn/+RfF73j6vXrs0lHPiX3kc9YqFmoUCWf7nt2pUmLpoFfHq/VP1Mc2tcuxY0mk8sWhLQglhP0/rNUgRXKTvqIKg21Y9o6HqIvv3mxKVLHqKXLkGLsrcZeqQZWqgd4LpoK7bQ0PUGmuEt1/qndMMha2ugDT2TZHbWa9jPrkLHAtegzfMuhRM4nQI13TtdGN09Lep3I+jSmZkZ9DB0r7liB3s2FJH3RW2zhDm+YKBJ1Z/2uIJV+BHeUPj5H9HOnhObt1lEqboD6sSZvlmdMiaJ0d0zdP2uQ3c0hxY1a1c8QY8PIsMt8Uh1RYo2KaU1OPZhHAw8i0uXMD7iG3OoVihVmapxBFTzQjZGdD989x0PUYzxviLlwJ+qGea1I1Xxeib0gmoN/L1mUwotpxSpDXvzSw/zGKcsYTQ4vFb6rlStWZ15KQvsiumbHOzlEE5F6hljPqVJipT6RCeTJhzVr2UfpWqFjgqmFhts8SpKkxRp6FfjT7QeWoMug01rKFYD1frZ1Uv7RrKyCnNJEG09qB8de9lMqcen75HS4mWgMrZXsduhF44eHd0+PDw0NLRi+Ypl/oEn+HNk5DH8Cx/CMxXbsV6r57I2NhTsh44MlCpEQdrY2NiT+/bft3nL6ttWs0HwiOfbtm7F6ydPnlRnxn4Ux7uAVd9rhQ+S1QaW6kqtBzUp/57BxsDiZS85d5YB3qROiROAIjpib+G46Us3KdFf3PXwTvCs9EySXg20R/NivBBOkCYNNPxpGJXAZE87oq2BY9PGewGzYfBCawT+6gKrgQ1sb/0+vksobTGoE2f68YtOfQfs4nM/pcVLoxfjetTPVJRCOai+uMx4sJvi8cizR5J6J/1VPMGVNMtZ9VsG2j7WL1WtAT0JnckGWZZ2KFzRyLGDFwdHNVzSwMnqGkhIqcWgJiU5QJd6MV4/gKG7prGqA52SekNHNKQ0Ql1T6Zk719/J3ploAPdPNSrTcNE1TbYsYFYkIWpoFr5y6ODBWDMYX0dnlbcDtwbGTiZWZZ6mxaDG7kzh+T/DWzwTy4/xUtS4HuqX6DexfQ6PcFOhZtFrqVvQR5XLGuqd/f7pWFZpAHs5Os0wgJmFyxEkagIQMLjihh+L15OaBY/EGP+NjTDhw2kABzbO53/08g2tW0bq67cV1Hi7188Q9BKPFuZLY9Up+yUe2f/0Tok/0ReVC8ruC0URaxUrVcwIU6wB/PnvftuUBTcL8zFJsy/44bGKlD9TmQ+qWeDE0mUNqdzXXnk1OnLpShXizdlcuhQkBsuEqsOgnj9+LF6d7t4BL4i9gaBGY0jsl9u2bo1Siu44H3fAIzV0YnhosXOtKqrkpRY0JoYUa/SOj48neaSGZhkbG4u1gTnSRQ1gpVRpAF94adQ+AiyVCnuOOKhKnfrhCqVOo2ZYbA9jJ9OVRuigWZjkuXGiIibm6U/VDBxUuMoYL64mGKW4cqBoAFVNSqU2C0cuDILRkUtN1Sil6mVB+MsGZeGbs6BGE5KUd8oYUlKwl/2SLlaoO6LPzScfsPQMXTm2axJU6DEuTB9sGOmz13+RpE5hrBp+Gu2FTM0SO3Ip61dXqvaeqsST2glqZAbVU6cvjSrvNNbuNfRLeqeGHmnozVSqsVElWr/e8usBztP4wwS85SioXKSWZCzkbhb1rtDIpVu/QeL+ubNBoCvtV8wc2COgtr4gKAM2U/92CvdeUYrHKDZJURMGMA09knOthulEBjxj52m4Dm6Q8d7hLbySqBeA8cX8u3I0CyNM9FQNoDJvDDa5jacq9VnaB2q0vnYQRlqgNMlB5cxB1O5VnVKlMYQOqOJQhDPJ+o06gcADZucAQeWK8FgH1WzSpzYLPjCpWfAuZiyFQNXdVIJqGVKSpN+eAyHfIMlhabw3FlT8Sw35BgtWP9C3Vt26MhQijvZL8E8HODaeNMDMBxVJik354ORTajKWyhPUj2jkXG8QDAE2oDL5waZxOl5Kv32gRnPxAaqXM2gBKjhMUgJqXpTp5jTS0Nui062xvRknoBc2E1SMYlGNSkeAqUiGn6aSGXCm3ixJhokClYlKhtlUuqlwWCzd1I4HftsHarSkQxBJsgAVrxuMWNUp2QXVczOlzQc1aW7GBtTQHKneLOaTY0ENaVQv8+HcWctJmo4vo+m1fm6GM6h2oKKjkMbUfml2SmOzAhhEbReoqaZvvmaxNH05l2YJaseL6LcP1Ogm4rGgRqO+9CENsxG5DzUhERtMaqbpa8j9KKVBOJVaIqgdn0ptIaiH99poVBZniCpV80RL7n7J0Gjs9IxXQXNwoKJxDBoVdkdSdK1Ia8Bs4eebp2cEVJdBnTmwJ1WjGhIeMNKXCyqtwei04SKoL40OMOGBc1ezX9yImuV62nO5oML1jW0NNYYKqN3TqMmgRpfOlG79Mla86+Gd8XYvjL0vblx87qe5y9uWlfAAmzNHRn6+YUtlTZtDvovBpAeGxEftikYNTc/obmpUqbJrlmLv6f0ympTvZaJ/cSNHoaCSQfWLvBnWuN23eUtZrCp1Gl20FLJ7F6dn4BRY+AWiUV3QqKHMJENePucPk1au5eiXnIeIVaeQTGtEqsv1hZ8ca/1y5AJClrVXUo0LlfgRysiP2r2ZMpNEo7oQ9fUweGCIVWSjSjVa3oE9CfZqwa6pL0ZNUqf2mTeVuqksIhVbhJEjF3MJY8s72FOKt3MyOdQaoTVuOqj25oYkPLhQdFstRrVRqqpEkGEdpg2lamVcbPU9r6hXxuXRlcaTuNdOrFLVrQybBI+kOlLwKWKXEKn6ZiEHVZakdisziaVGQum+Bk9VsYoXGfPMhCvPpyeWVDQ0cMkuXQIhAweV1i9XF0WbQrcyxsbG6HVn9dLxJDpxavJO/YqEGFu9xrEzNyTX14X6gypkEuupxtbIV3pVJfSaI0x6RqFKUjcVnvYjJc2pmUTrN6kmo7KBcYIavAwNohclhBPBxjTXyw/flyz1uGX1jAtJ+aG+GNMnkkv7qsjHoYMHYcrqNbhDB/iEqYzT+BbON8R29yBwksUBqyf2S4MztilC+3rAiL1v8xY9+Tl04PXVt60Goqpefqz9H0sp1SlHMVu7V9ajtm896onjhhJeqspzuGcksBraZwX6BNoS3hrrYkLwBH+i4+rVCZM2g9Ap/fDdd+ztunqUKiseMxUkSa+qwYs/s98/DYsDwxNbA4LneAWvUwMn7UCTFOlVYaRMToHUy2/lwnFDX7y0b0TVyLdnNWnbstBh3taN/w3m8dERrcth1qZUcT2ep+oPZAZW9QiT+Ujay8dAaVCCMIt3Crn49I8E1PZddOKeqJHSvkms2myOathUKskZy1G2q+7w70IJuExNoVrD5mS9KWJugf/tlmtQJduh5TWTHtxoLgmv9nGLZbWs7cb1Hry4bVmu7VXqjyrV0BRRu4a2Ro4dtGRPt3YW4H5oW7re8Le7jneQ/Be99Fd/y93cW4+rPZGjG5Y1yuiNevKeAbzAqlpmVG5TxLY5J5Zz7EkpdX0dKReaGDhJZpX6hGEhe7tO75R4L/c7XqQ0l7oYgAG8dIvnspoiSZGq+ZhMkV4BtfWgRtN9Y51VL7Dk73Wd1HtUx4p20ySJ4ZO6wlfgweafzdzCeCmroIVbyIZ+SPGmiDayZ/EuBJDyeQQdz3Zwp1J+0nagIIe9JGqGhXBV3XTWP9AFdeGL+G+YzwWLjsu1Gq5LQ6yqquWMLRVpCtNQ6LsDXOuXj9Lpe9YIqK0ENT6LMMEGxiNdMtUdU4kNoRh9MaorOPmh961m8qlfGK/Z8xF8oz3002yawmToLhgaaCK6A7mja5Ob1nec0g6AuqA6vFCn3x2Tpg0Su5rhX1Ckn13VdcV/3rue+W6NBVVd3tzm9Yt2x/AWuqzRsSy1KZIcCipSWNfBcFnAHZC0pEl3dnPTNAYFHZHYBHM2vhkcTPf7QSaDHkjREn4XZE45ujh1Bb4Ogu99b8NteFRf3UzBRXIumrhyoMGIQwOBatCmKaIYcwKGvgY+MGRo8Ot4gzJYAZ1PS3IQVPYD9EL2RfJDbNgdYYbBMQuI9SNAoVWsscK9d9mJ8V5vjsGfg4HwK/iNraCUl8erhZCfYCwb3gK6MADpxNpoUZ1PvN3L9/AVKQ0NfRRDQyXmq0hakmvzqHEbGesdEYLe8JtvrXrnm7eovugN5340OEhe272DxAadTAmApGgv4hzyCc7ZpxWi6H/4FgjHhYZTqis3XLlqIly2wpUT0YANhmtgQRgbh3NgONnj84Eh7+2alYFP1r8ox0AmaUlugqpbWegZb635OgQdRRl77JEBsTh5945PnnrC0yQvjUIboMNR8Byv4HX815t3WYA8YN539vCxb6xbiUf1jS0SwolfwSZSFKn2YWE0/PZo46j2CRrHL1DGt6j2oQoFn2z/Rcc4o0haUltBNeXlJ+MKotBp0GPYI9lTOf9OAtnPdNFfZziEdjX1Az4QH9suRZrURBxxSJRqHxUZjm2c0L/YAsrv4AdyFAuFDDJPop44LqC2EtSJM/1Mfk4IV/ZIBa1ynIIgB4vi3X0H/+S/2PMUn/wEZTG2FNGQaqUNn9Q+KkqnN47CUm8fNrKuRYuMYjKJ2mJQJyN7GVv2ReKKDqQTq4SuZkj0E/gW3e9tryKNHc6Ub2/ZPtHG4WnUycoGLiK40UJpi0E15+WbcWV3JLF6b+PzpL6oNIx7iCbhGvr5oWYJgcr20QMBpVzPR49sF0pbDGp6Xn5ad1QBSXbKUL+M9j+lIlxFNNo+DJhFRzQdXd2nraJ9Zg7sEUpbDGp6Xn4WHatPxNMpVV5rRf2vdcSGWkY1jt6AleQ8PvdjobTNGtUmLz8vtEo4JdtBPi0bp4bvlUnUdoOaKd1XpL0ik6jtBtWcRSjijMgkasujvhbJSSL1lOGvbjIZbvDEmb5Q2mJQk6r7Fpf5766d/dqfNaXCfYP5RBOhobwVrSu/WtGyvqkHNwqiLQe1f6L0zsH5wzfWrbzyzGFuXoQuiL6IR+GWZLJBgoFseMsnTz1xbfxXR549svq21RjgyndQZRK17aB6sml96aCiI37vB387+8WN+fn5z3/3W/RCb0XIQtI5+yi5dR9djUyvWNzKYAEDRrHPXv8FGkfVK5/4/I+7162vAtSLh/cKoq0H1VDdt4jdiz43celSqCT83Nyc4tYrLDK8BfpcqRdagCpV3XIL7YbQSCApOpbcdVaRea1/Su1MEzomPrv6zIZvVwGqTKK6ACrsoopA/bv/839T97b4r09m0HeBLvqxh66vdXVF5Kmg5V+mBlZKuG6MFxYY6OtddBqXWPX+ijZYs1defB6/CwOTza4WOP7hzX+uSKN2fJdxR0CdObCnikScd755yy3f2XZ1Pttx1d+IBfTe+GTa0739UxfefhM9fnE5KzDw9TDcYBKigFFqWb2etKwsaaGZ/oGU0BgR+PM+ihhWcFWkEdeJq8U135i7nLr1Trw6vXTpv31t6K01X68CVCnn6wKoF5/+UUVR35u+dFOqUs1xsMomMQYkIBkmpYIZEmhmJT7bgUBdU/QXffAoeC+EK+Ch5PGxnr06/itAOD95zhtBfBSvlv2L/nTlX/b+5GY0WhUzNLLAzQVQK0pOovXbu3mtpeFnr3K/94O/tVXOvlzXDmBG0V9UZyaC9MWNKkYcHv3+aehSUFqR3SubFzsCakXJSUxkRf/7i7/aVWK3xqfhM9+fuThf47H/f/3vXq+HrwZUJX7s5ctXvE/+yjcgYPWNdSurAFUmUV0JJiVtPV6GUn1mw7fBlaUOtALmT26G/ODv/mdtlELlenYpcMJXf+Ub+C1qd/Dcx+TUJH7L/1i72fvMm9eCUjyvqF6UTKI6Amp1WYRcGkLvC2jli7KorX7xCfgc9GnPUPzKN+DX1QMqyMT34ldAaKPi22/5zjaQNnHurP2PwpkwBP7hzX/2jIKb1/Jz1MdWZPfKJKo7oNqXOMunVGHRsX9DaaBzZwVsZmZGKR98Dns2ujhegelYNaW0tPmlugQK9ua1uAycA5gxjsCPBYe64BW8jv/iHJwZcK79EFKKx+l71lQEqkyiupKZZNh6vIwsJfS/oaFADbKPouOiExswg/6EssI5OFMpsTAqPvnVsQrrlKNDlFKdMXqYxC+QBSM5/KLviMZ+SHXqVCZRnQI1a4mzrEoVAwFNVg8wWn0+ZkoXQWdCdOWjTsOTJE74IeUGeHiMHh3lBRgoLUWqVqcyieoUqPlKnGWdqgnpE4+EkNpJUz7xJmhJAR4v6+DcWT3GUzWlNahTmUR1CtSCJc4sp2ropFXR11WABzoZBjOgheEKqzg10oNzaGPjjdEYT23qtLpSSTKJ6hSoZZU4MyxchtLAY0WsLgnw+KRxwoPWNQ1s2ti0rmlgA+wlNra1Gi+L0urmTmUS1VGNWkGJs1gDGI+VsqozEJjW0WCPZmBzDrM2OEMXWdFaGdlq0VlQ6ylxpkImNbDacFGuadUlCGUS1SlQ6yxxVjWrgS7V1WkO8T9BRaororSG7XakSqhbUd/jx7xln7Us7yxRr4bt24VwMb1TuKDKQR0ZeYzy5L79UeG/6LhS+Amxn1/QWq6T0v9Y/+cyieoWqOfPXnnm8B9W+Qs7a8GVrN65/s5MWiuEDZkEV8Ds0MGDo0dHX3vl1f6xMbVAdH7ynBJvsWiy6Gd6b+z/Py6gw6fhM488ewQ8g+TF+FN2bvlL66GUOynjUeZm3Ep4+HBq9osb6Nyf7t4R4FqLXuX8qpqoSE0AwnPqSWAJfgASiApBGEKO1PHRLDw5eEsc4fxAAAx6gS5GhyW5GcZJnZoovfuOYBn9A0OX9o3gaj+++JHA6UpS/vn3Z2ZmZnF8cePq/DxxXSzDVfH8KqdtmA4RiytzD4AEFOaH776jw6MrwBpEZ1hdAJoL3ELfesT6CjYWUdgOHwzdXiGld9/BShS4dxhKLl++grvpyewszCXh0wVQJ86dnfWPJbiO/8qre+CbTzWYwczdp1uocKUihcEJEnSdVhuZNujq0AaJh5pq5W/hNExFMzG0coEoVChGDa6YB6jB3ZydhbkkfLZ/S4uPp2aXHrzBLIzw2eu/8FzW6nFl7j6eKEuYgRxYmF5BBmDQDDJNAmLnLmNMoTHMn4CDRn4VilQ5okmIqgNGkyDablCVOo3iiluOG4/Od+WZw6pP1OC10hLmQrbAvm0+pQuC0Y2r22HrMuuofEWqHNHhLbg1XhMtpEPO+Ef0buIuC6ItBhUD7WzywbvOSkLoDfXgqixheHSwGL3SfnOXWwOqr1Sf//vDCtEy8xl8PoPYwfAW+Cb6kvRFzyXhkKhSi0H1gr1ph7KEFa70XcsNNUX3Rwp0kR8aWQwdNRtRPHI4KxfRxfHRr+JNK1chalCkoeOD998TUHuOqdOoalWVGQLfVeFaeN71vQ23/eZbq/Co78xNCQpb797B3R/0kFIT4GQkyYv9+gXEP31gqMy8kQUVihbAh3s/ObL0Z2bhsLmPElVqJag26jSKq15RwYsM794RzAoU650wdMEqheUmFon1+6v3Ff7cIKvshqY3q0Y3dmKGc6oen7t38CJDTgGVao49uHQVit+rvNCorWuJqCjVtoJqr05jg0x6p1G4BnuTFViwij4NUN/55i0QEkuTmMRyh4vgK/xK2cFuLuBW02+xmQ+hFAhSpz9GJfRRxNIj88XnAzgXNqrTEVU2PGv549EeVG6fBUQxJMWqUFWnJgeiPC5cOC+guhDstVetobLa6MEgp2C0ib2cuzaGiNV1rNqIaZGQhT0mvNr2Wnl7JbHpR4vQ+v/Sz2fpfX5aUHcfXxH96gU7Ql05sIQNDyGlWU1cb6Jl/FdJ690zeaRJR8fzH3pdUKcGM3hejzY9MMQtWwoqWLqvYPWtNV+H0I9F71dqlmzoe6jFbCTFz4zuYRES7ky1sDlV6AOjO0Tq10DTXXnaRBQnmOJJC/sXm03c0EZ4BREVT7VloBZRp9GAcHQTQUabPEW0aV2w1VJeD1aZxMSA0FLN6lorhG7s/mv2Et0njtTpn0/LlldF4fVEw9dJiUTcvxh6O8nELVeRLjGAP54SUBu/RvziR7PlHUq1xlprN+Yu04MtxSRWuovEvrFuJSTKbRRdBZgSFelRj7qE3kUsdTKp4dX3KiWfqEIXjFuVpaBPsaRWdSoR0UW9OvV7AbXBkd6p389WcMR6rUuL5HoTjFSwBVMmFLFKzYIZQqtzS4RIrwJYSTQpShe+hW/nR1GT8yti3eZU5zPgM9n/jFWk+UK7ElVqPajTpapTe9Wqel4wn7FgEhchNhS/CeFEpacLQ1Nm4Wk8X8HPT1PKM4XPBecz+HW7d9gYt7UpUokq9TqrTrOpVs0k9pxYf3FW0Kfz+rG6YRyCNhbR2FdiwQ4FhxL5XNgcPXA+F+Z7vWmeXHtSVadIJarUAlCjC2UqVa02nfLGJ9Mg1ttK3M/pKQ6tTqxyKZNQjJKsDOaQoxt1Pkmm2uDcu+CF/L4ie2GVFdq1VqrvC6jOBnvtUw7ttzD2rOL+KW/20s8lXmI95oJWZ0z3PGMlNgQVhlObUFlCpj/vap5Zsdyurh5F2vFVNb1OBXvL8loNwafAm9WgDeUY5IA2VuIDtgvzOipay4CQl1Px4vOlkFmzRypKtQWgwjsdSCew91oNmnYR2t07YCHram0R3dyZxtpca5BR7GMZ6EyS6atNXEYRmzbVI50d0NE1pdoTdVqK15oahVKp8EFmn4+uzhgxS5I/rPrq/HfX4lEBGTD5wNCS/OH+qXJ1ZtMUaWeXqvZEndp4raUrJa4CBVdBuryfoAveAow1UZnAb6xbSfM1EJ/JKrRl0zxSw93pTv5Dc0GdHqg6rUi15jv6/dObNt7b6/X+4q92TVy6NKjLaIgi1Y/p85MC6iDlg/ffm23YkTUgXMpx5NkjQ0NDvaUHXsHrnVWk3cx/aCioWVeHu6dax8bGVixf0Us+li1bNnp0tOrLKD2xXqJK7oCKMXK2wUdFXqs6xsfHo1o06cCZOL+iK6k5jUFW1bQM1Gaq0ydvjaUAAB8uSURBVBpUK7QofdGsB96F9zrvkSYdk1OTAqqo0/oCwvko5bF9eDiYvy3PI51tz+H8VE1Pgr0DzIvQ/UCvbvi1a2a/1OCvKmcS9moRXFukSDvlqfYk2DtA1apHU3HgFfx505duygoquineO6MdxD4foq2jtAtKtVmgXrhwfradR1avVV8UpsAgq+Pj45koPXTwICnNvcCgthVqVR8O5z/0RJ3WHBBOnfDAOU/u229J6apbV84n7LBkucCgLaFdq/wHd5Vqr6XBXhh7zexb7PTRymmW5qVSy6tvW20DKudmzE2RpF1boUjRKzKtc3RVqfbaGOzFnWvyFE6sHrP3AJWzmkrpiuUr+Mk5fOm2KFIoyenzkxnmVB0tqtRrozrFyW2Za4USy6G4yCqcT8sYUo7QV1vMXYCatZ6zk/tf9JqhTrPdCYyazQdVjxVlRUIZwNu2bk2ilOm++WBrkUfqDcpTv89m/bpYVKkRoGYttgJbqBWgFqEC77ruH7Ezq/dt3mKIIbl0MOso63SAe/UfBg9qjtplMIdaBGoRwplXGAWV6fjOUxqwurSTMDmka0p1wKDmWB2OIfbjix91pI9ypVtSwuBsNw4GcoM7/sUNsoonZoPCMaXaa+DcKYfMpFETg2Xx3aJaBCqYjIK6+rbV9vHeth9EzrOhvrhx4aVR7oJ35ZnDHqXg1kc3ejiWqDR40xfjJUzZ92custEVosGThTuhxs6PvTBgzsT9oaEh5Q+Pj48/uW8/nrz2yquHDh6E4sJjoxS1eUKVTmwXQOUqNjx+/rvf/mHVV4Mabsu//Ondd1x87qfENapdHVtQ3pjpmanf/9Pro4sVg/yNQyGfPPXEhbff1O8ExtfcoIJMYMnnwBKCJ/D3Tp48SXTVf5twgEP85KSoL9e1dQFUpRvREzxEIxucA1cO6IpV91aoNigzaebAHlUFVxXCDLY/3L0j8Ez80EI+0xc0gsmRkceon0Epn+ugovc3yu415P3Gpvg6eajEwOnzk9EKycQVPYT21+TUpJMlf5sC6sSZvqmG7fIvQ7XyNuTe5IKgon/D+gWQYECBihfxCH3bqH6fFEnqWjxJJRvBkmKZ1Wgn4caQ3rydpBBWO0nzylFDvWmvnu3wFgyZvGf56v0SVNq3NHp1jQpE8WfTQMUlJYHanXiSPtfieUOa9asLXkcvkqT8yu1eA6jevfFtG7or+RaXA1GauOj9BBX6CtpVmb4AmE+aA6q5eFJH4kk6qL8+/kYSqJDzD20TUCuep/E32E0yfaFRGVJiKC/fslUFJCjlE76iXu/3TwPdhvTOubk5DEzLli1LXTrjPKh69YYP3n8v8IYSesuH/RMCalXy0bGXzVutTD24EXcI6pQeSBfSkkAghg9zUj7TfZ1vilBF/I8e2W4A9eOjRwTUquTi4b1mUC8+/aN2lSksBVSoejOoux7e2UFQzeEMYCygVjaJev8GM6jnjx/rIKipdR66E0/SV67BuDV0lel71kyc6Quo5cuHJ46bKYX7WtbWxif9IzRawzUNncPPx+PJhWMgoMYmD+rHTV+6iUteOwWqN0/z0DaTUj32soBawd6KR4+YQZ1+fFcohykfqOCNYSROpfLFsbExTtKQTJ7AtAdYnowDDwrU+zZvSS3ygFGmC9ZvKB9w+rkfmxylw3sF1AoiSY9sT3FQl7Z77u0YGeDVnzACDBqpVFX4NxQlrv9gGRebskkdCfyGQIUrZOgwk5vWC6g1JiQlxPFyFyuMalQmEqpZGbwI7argHCCooHRyatKmuBkXpnYA1CVZgWY3NRrUEFArd1CjjV4KqFBEtHv5JGT9qmR9phbWnwBsX933yX37OwiqJ5vW2w/uAmq1mYMM4pW1OY1u8XKBGx7xCjP19ZASFCz+HKBGNWf5hraH6gKo0dUw04/v6tQkTa/hM6jRpLDioNLWhd1LXPVXlOOqpxYOBFRDlm9o15mr1645P0MTA6oxnhQd3wXUYmI0YGIjeLlBBY00ZSFgEhDS7lValCSrjF9YvHiR59cPqv0WqRhTnM/4jZZrSM1mc8xN7TXcQY2uh3C+DguzfO23isLI4rz1GwX1w/4JqE37bDYBtcAOi0brJcixPnG8a6DaZPl2LZ4Uu6mMOe3BsZU0vSbPoEZzkjoCamqWb7TGr+Ognp/MGuBwbDZ1oBrVaLokDYpdANV+NzfuQON8PCl2R5nUnDaXlrwNDFRzcokhF6wLoNokD3YqnhRbUDu1C7mU9NtrsoMaO23tNqhMHozdxsJwOF+RMBbU1Pwkl+JJg9OoxkiAIcLuNqigFOqxl/FgRcKugZq6QHLqwY0CarGJmbSx0LCw0G1Qwdtrr7yaFdRtW7d2E1RzfpJLbupgQE2drTYMh86Dmrotamw8CarY4XhSEqgwbjvipg4G1NT2jS5D7Q6oqevFY4/JqUmH40l6fbNMueLOuKm9xjqoSU3sMKjmzWa6HE8CqLGVtdMDv65k5w8AVHieqTOoscmDzoN69do1w2YzXa5ICHshFtTUYIcz2fm9ZjqohqRqh0G1X4batR0ukkDNURlPQC3VQU2uJZd79UwrQLVchtq1eFKoYmimRNTp534soFbloBpmwNwGddfDO3t5D4fjSQZQU5c0J0UlBdQSHFRD47oN6p3r78wNqsOFzgygpqa4wTYWUCtJ8TVH1XPXTGr4kXUZaqfiSQZQU2do3Eh7qBvU1BUP5pCvw6CCsX7/dK/A4fAOFwZQbYZ+B9Ie6gY1NecrNVKXu65v80HNtAw1docLV+NJBlBt0lEdSHuoG1TT9op224d4N2xq0klQMy1D7dqOqaatxNMqbzmQ9lArqDaDn82ih9x7zzQc1E0b7y0IqqvxJINGtZlHiC0VIqAWjSSlxtPd281tbm7u6rVrWZehdieeZEh4sPSnosW3BNT85bYtN/lxD9TikSS385PMoKZOpZrDkwJqnpwkm/0InAR1bGysOKiuFs5PWj1jXy2k7flJ9Zq+FjlJNsmZFy6cd68vXr58JXf+oIr68nMcjCQlrEe1t9Tanp9UK6g2OUk209PT5yedBDVfeQd9bwt8gpNzV2ZQbWIfba8e2mtaJMkmQDd98SMnA5uzBRbQ4O2uUpoKquVsQqvzk3pNiyTZFDj/2EVQiwSWTp486TClswl1fbMmkLc6P6k+UG1Cc5a+hMOgzvgHqLPcyFjtPeMwpbMJlfIzrUpt+6ap9YGaum6QMnNgT7py/nhq1umDrEJPpuboq0KhboP6cdzeM5mL+6RN+wmoGSJJNmmZzoOqWL18+Yph4Ruz8HGO25TOxu2PmiPnwUYHdB1Uy0iSpX3i/K4WOqtJqYVDQ0P419zcnPOU4sAdL+5YtXqGpteoSJKlx98RUMnq9evXozVEVyxfgRfxry5Q6oN6tvgOKa0unF8TqJY5SZalqLoDqs7qyMhjpBSOKyddO0IprIYP3n+v+Dpn86osATXDMlTLpsRtQ991fq9BnVV0VmUDT5w76zal3ClrfuHo909XXdpSQM0QPbcvbwNQx8fH0V+VtnF7QwfFKh5dnTLFr1O3Er8ONxe/dPTo6JP79t+3ecsLL/y8lCBIe1Pz6wDVMnHE0t3v90+uvm31nevvhHqB54YbiduJmzrrb4XGMRjK1mG9yrlWl9QmK4+TzEMHD27bunVoaAi3eNWtKyF4DlZ/9vRPSlhB2drU/F6jQr6pM12gFHxS1L1csXwFnihoqWzZpzlC4/msHA0gk9nIPPAnbhNu1pFnj8D9Bpy4ibiVuKF4gpurbrQSkzI4cbysWfrugmpZ0MxmbgYmkLptGGX1J2oAhr7Fn7j36AGvvfIqxmmM1noXEW7r15n0TUDm2NgYyVRDLeBUJlLsneXjocNPFbfa2luTpdeokG/q3Azsn9h7GXqOHoB7z+EZzzFaQ9/CpiK3k1OTSt/S7upIUKpSJkMRIHrU/f5pkElXc9fDO3Frli1bdtOXboLgCe4RblkslqE7C4ExZQbVJqOmvTV+e80J+doE5V5/42XzuBt7j4ErOwcE9IJbjOjgVtnJyv3TD9rMwnDUfNWBVA2lY4m2RQtjcOSIqVs6NHzwnGK+fepfe0YeTY2DWBV2b+0MTR2gTj24say5mRMn/yVVo0YfVbdgF4n2HnQphS60LnobAEa3Q+cLKQo1LeQwwPx1ISBpu6p4LNqHTBqwDLmaVJ48RwnOiSrV6HMM0Ok9La0WYatnaCoH1XIJkn3iiI1GVcIQRewQHgpK6T2MQpsZXRBmG9wq9Eh0TfRRauBZf5fEqG7Bi9TDjSUZFzbnH9f9Q794Xj8jzASSNOLnKxrRemwu1WKxWMYOpmRSvT1JqUafYIAuSx+0dIamclAtw3H2qZhb7/9ekokb0qVRdYouAvZsPKIowKo7kn94XAAY3ZcMQw9DyaBne7PzU5MkWcEQ4jnEhsJbP4h6rITO5Hhh+CL9Wzh8kENcKnUjLl7RSGeSrRT782Pjsal+pnrCqRcdVA6IBrxTHdRMhX5autitclAtU0bsVyHt/Ou/sdSo7BOpHpGNIR0LcKgTs8/xe6F50N2pjdH7dZ6VaX3SP0ALBNhQOEdK1JMO9V884bv4IfxAGqU4iJ9OIIRaEVeoQuX6D9Et0lT1aB7skljlAKqDGgopxb7XBlTLUEhLq+b3mjM3YzkZvWfkUUuNqqtT5Q6ldrKk2IYNw8RYJ1mHWSdBwaB4YGcFQhTglCTqHPLGy4h+b9JX45zQVye1YSpyOTRq7ADKlIakt+C/Nh1j5sAeh6dSe82Zm7F0Hg7s32ujUUMmlpqnMRCeg8/o0GD+zCSw9YMUEadY0U/TD8O3pMKTxJj5XWb3wVKp0iUxYA9np8QSIi2dSu21aG4mBKqhq0XVaSiMFAuYZQ82aFQbLR3LrQ0z5iko88VYmg9Zjdh8GjXJU036dZag2qx0a+9UauWgWrr40/essSwSR1DNPYPTAMq4MqvTrN0xK1SGrzMrsVRgLIcDe/OhRI2aOlqFPFWlVKPnW4Jq72RNblqPbgkbGG9pS2nC6udR7Wa37Me5JI2q32DOyiSp09I1qv0TsyrO+vk5bM4maFQ+N4T6QuenZjtkBTWkIUBs8ydXqwXVMl8kk+eg0n3NHSg0WlOd2kdKMvmoWZ/Ya1T7cSTruDNYjRqbi6JPnunnWGU7ZImGtHFhTa8hk6j2sbiJM31znzMP1Tns1RI1ar44VkM0almDhT6najB8MkWS7BMeWlqmsFpQ7Re4fTB0O0ZES4dBpebbTNPpEcWGaFT7maGsdmm5Q0Zuzi1PjirVUPKDZU4Sh++ClDa8SHe1oNrXNNPzk1LnaXBXkvKTouHEImhVrVEzzZQUJCSft1mpRo1NftBPsDR67edm2lupsNeQbIdobAk+g2Hz2VhWbdRpbh01EI2aaWKmlNGhLI1qeT2G6e7U8is5DLeKZlnRG6H533rrlxCbJKrGgVrQv+cIlxSRQ4vorFqmvDRco2alpeA1ZJ1bKvczDTPe9pR6NbSGbi8L1NSVIWDynXfehqrHFcIF2/XI9x94cGvo+u/acFem628AqCUZJEmJ1Gg1PfXXkERavMPVplEzAVMWq6VfsP1p0eE1Uy+33CrFUuY2r9c1AZkEkI8/8UMACQJDHcwgONnedB8wqPZpSemT1BYpEIacwSpCtSVq1BoIL8VCLssuMMzTmIuYVaQJKP9573qAumfk0aiSzC1lqdZqQS1rtEtd8YC7G13omLoiuSAnDVdWpX9mdYqa9w5jayYVlCNUmQrq/HfX7l63/r+vsuXw9rWLj5WyWvH0jF3+YCkz0bBS9IEwNmewXE5KJGdQ1my+KeVyBwj+iaE2UximxACSLgD1mQ3fjoKqA6mwNPOpCzpno0EtOAcNzrPmduF+m5McSomUDNb9q0KLDvCHb73/ezn6MYwsQBVShpQqQLURtbaJT0L+arNBtSuQn2821aBadz3y/awkDJDShowOA7kAyzxehnbeeuuXjLU+/sQPYaAqMiHwLd/bcBtefGPdSjwvAio+IRVUfb1haEWxElUNk1Jw2qbipPxNOZuseJK0XgG4lOl+N540J4AMRQrwDHOShvkPCFgCKiATChCPELzYu3ktXgSoULCZAkgQxfxvvrVKgRpaAxwq1BbCMtrf9BpRsT+2KaDmnuCC0Vv82zGGHTr8VA6fsyNPBqJRKSq+AibNc5IGAU668BUQGwUVf0aBpB7G69P3rIE2hiKFEHsDk+bV+Um44gMLTtVUPD1jt3Sm0rKO6Ar6yrh6orht0a51/nw+x9CJOwKB+RqqTZNJYgM5BFVHkTQqdKNMQtQV3vKdbRA9E6MssdnnqrWglqFUde0am8rfWRu4ZqUKwXCJuwBEs85Shpg0xFqVRiWNQFHRqAOpmCSW9Yj9MqCWgQopffW9jms3zd0cdSSKI7pn5FHGddH+Bi2aY9oj1o2Makg+r43JJCli/TYa1IoqOyZp124avZV+i0LUntLisdbGin2Iu2WgVlqHauJM37DllMPB3hrsfMjOv/6bkAKxt3iLxFqbLEWs30aD6lm/ySvdyhJ4+SqzvyO+aKUaFXojOhUBvWom06AkWwekoTSsa9Mz9VeyQU86dPip0Lo5l6ZMK411U10c2L83qSDDrke+n0QplyI6xmSS5E57aFbCw/Tju0JKuP5yydAGsYUOHdCilj5q1q9Dc6WGSZIo7QKcuuRO+m1WCiGrOuisAvWBVL7AyAeTWN8+o+2JR7E7aMU+2qtQy0x6qFmhtGDgt1mrZ4Aoi4xePLyXZvPAC8NFTWJDFzfXp2/CvGimOXqDF5qpw8U6qKHS+B2R3GkPjVuPqhKSJs70G1XFHCZx1Iktpd9X4YsadGymCw4FctHPcnhZeEs0etRBSiGZlsU3usJD87evhDIJ5SQOllX7Uk+5JxUwQhVcUSnqlIKWbCKoqTvhTT24MRQ9asv2lam7p2XaP6oiX7SIRoX+xPBffMUzJVRtqJQRRP06VodoBai5cx5qrevrlRRc6rWyyGBbqpXryRJZQa20dEumjZhtxLLstb3o2Q4l2r34HCD65L79o0dHVyxf0XwtjeGvBbu5eVvRLN0bj9UZPzr2stKrudeLNxxUy2hqWUtJC2rU0ovThuZRS0whAJ+XL1+Zn5/fPjzcfFZzJyf1aujWYE/tbxfajUYlCXqR3qd/1HwHNTeoxVMpCk6TDlajqhI5JYKKn7Zs2TLo0nn/AK54seGsotug8zQUVEOJx5bu054VVMb64O+98MLPDx1+Ss9YtN9nLd+GiKqQ6mBBxQ8v10fF28EktOi8dszOztJfbTKr+ayV3kA6OjOQYBW3ZRvZgqDGxvpY3AAM4797Rh41T/yk7i8e9YUO7N+LD8dX8IuaBmrxABJAnZmZmV96TE5NAtQms5ovPtcbVF9vKaIlgho734i7qMoFkWEKqIsK/8Uz8RbQiLfHDtj4zMGCigvQg0nFuzsoHRsbm487Js6dbbJezZec1GsvLe6BWunc72BBDWURFjd6n9y3fz75gKZlSbEGspovOUlAFVDrADWUnFTER+VS1bm5uXnjAb0KnhuYWZEvOUlAFVDrADWUnFQksXHZsmVHnj0yb3GMHh3FyblnvBuVnCSgCqg1gaonJ+VTdNytD0+uX78+b3cAabA6WDJ52SrNI19ykoAqoNYEamhnoByg4l2grt8/PZ/lgDdLVuu3gTkxhm/HNUAYRcuXnCSgCqg1gfr4Ez8sCCp8TkujN3QcOngQ7x2IIl1160peM5MccycnCagCak2g6slJWdN9Gend9fDO+bxHzazygvEzT548qYxwdQE5ch4EVAG1JlD1nIdMoKr1MbOzs/MFDhiftSUY4ou2bd2qX7DSqPmaV0AVUGsCFdeQW6PCzXvtlVfnix1Xr11jEn/VrNIpDX372NiYAjVHcpKAKqDWBGq+5CQuZCti9IYSIeg3VsGqHjqKfvX4+LhaNJsjOUlAFVBrAjVfchJ14OTU5HxJx8S5s1UkA3NAMYS74KyqX50jOUlAFVBrAhWNljXngSGZQwcPzpd6gFXcvnJZpVOqQkdJGcjsNjmSkwRUAbUmULPmPFBH4XG+goM2cFnJwKB0ZOQxcxoGvlHtBnBg/14BVUBtLqiZch5o9EIRzVdz0AYuUhdGOaU2Ov/y5SsMX+dLThJQBdT6QNULsphBzZTTm/sYGxvLnWCo1sRaXiT0LWxjgpoj50FAFVDrA9UyOalSozeaCJGDVV4hJGlNbOyx6+GdKp4koAqozQXVMjmpaqO3SDIwz+GinKxZx/Bj1QxN1uQkAVVArQ9Uy+QkUKpKltVzWCYY0illIJqlD7OOCArUrC0soAqo9YGampzE+Rhonvnaj1QbmE6pXvcw66Gn+0a3kBVQBdSmgBraLSrJ6I2WLKvNBk5KMORSGPy3SCajnu6bNedBQBVQ6wM1WpAlazGkqo/YKt7RpTD5DkCuQM2a8yCgCqj1gWpITlJlEHL4fiUe+HY9GZiP0aUwuWeDFKhZu4SAKqDWB6ohOYmZA+Pj4/ODPpgIgcsDnDTFy1Lyel5+1uQkAVVArRXU2OQk2pZVpzfYH5NTk4wbFXRKQ0e/f1oZ/FkLsgioAmqtoMYmJ1F9zTfpYA59uRqe/LPnCKgCastAxSOe15beMMBDz8vPmkUooAqotYKqZxFyKpV9t3iopvmHnpcvoAqoLQOVpm8XNOr169e3Dw8TVPQfAVVAbS6osem+q25dWXCKsi2HyssXUAXURoMau/+iYWs2xw6V7iugCqiNBlVP91U5D+XOgjT5UNn/AqqA2mhQo+m+BLXm5TKDOlRevoAqoDYa1FAtQs7QNCrbQUAVUAXUcLqvArX0UoMCqoiAWnK676pbVw520UzNK+kEVAG1BaDq6b6cocGTsmrhN3welTum5yibJKAKqHWDqmcRElQ8bh8ett+euKWHvnBccn0F1KaDGs154OPc3JzDlI6NjcHCV90m6/YzAqqAWjeo0RkaNqZL6b6wDvBzJs6dHR8ff+2VV5nnoDKxcmw6LqAKqHWDGoonEVR04hJ3gqrzuHrt2szMTL9/GjoTxu2hgwdHRh7jonMWi4C5y2wktUoe/UdAFVBbAKquVPmNDU/3hVkODQkgoSRxnVCSR549Aj2pmCSQFCpPtaJNF+jSHNuNC6gC6mBA1XMJG5WcFDJZSeP24WEqQ2p+nUm62TadAYjm2BZVQBVQBwyqStBXlc1qLucbNVnJJJfdKRpZP0lpSMv7jtNA5p6RR3/29E/wM3NsMS6gCqhNAZU2sF7noaKKvpcvX4EDHGuyslpvqsmaqi0P7N9LJt9665f5jFsBVUBtLqgQdPGyivqGDFelJDkDlM9k1ZXk1vu/ByWJuwkm0aQYZapgUkAVUJsIKlSQvuuM5cLUpFirwXDNxKRuuOIK0SA5QrUCqoDqDqgQIKE2dwFaAA8EgkOYrKQRGhJAKsN118M7SzFcSSNUOpUkncnalKSAKqC2DFRIaLu0UI1C3WrNargO1mQVUAVUp0AFOcApBJgDJquAKqA6BSpblYElGwHV0VirG0wKqAJqo0FVEzbAT9euDhiuAqqA6hqoujFMkf4moAqozQVVREAVUAVUAVVAFVBFBNSGgJqp3zcB1Ps2b7EXucUCqiOSSaP+7OmfDPZq33rrl0NZDlxw8QUfIgLqIOWFF37+wINbb1+72l7u2nDXAI1J6H+98J+l4LLxLvxYueMCapsEGgZ6BsiRPXblTI9QawO58qzDSnSUefyJH4rXKqA2XdBHdz3y/SJ9XUn9qTMYHUq5csjADXgBVSRR0DvL6uiQ+i1JKMMSrx/adVB2gYAqrZCYGVOWIlWCD2yX3SuqVUBtukdK37JcATY1/5DSx5pBjTgCqrRChX5dSGCI1h+jdua3CKgidVC6YvmKgaSY43sr+kXirwqoA5PqKKUK+uD99yD/fvoE5NSpcSX/+q9vU959d4zy6+NvmEWdCeF79Q/kV+C7Dh1+atmyZVX8qLs23CUdRkB1R52C0jvX3/n83x8+duyFF1/8WZ2Cb4Rs2nhvRazKMjQBdTBZR1VQuurWlWSmZkqjrJYeIRNQBVQXNCqLcR06eHBQlOqsbh8eBqsluqxi+gqoAxOVJFgKokNDQwOxeJNYfXLffhjhxXFlnU4JJgmog8xzKN6DIfdt3kJF2gRKdVzxCFxZlTMfsTTmJe1BQB08q1kTelh4lv3eW4DaPERDqhWCi4QxDAWr17BOcmI5APEHAvJ/en1U+omA2oLlbKGS0HgFfO56eGeTEY0lFk9ILK4/tEFLqOY1q+b+4z+O/vvpE9I9BNTG5RKC2F2PfB/Q0n2lzuE279u2biWcdETbgmgSsYSWeyuNjDyGnwaB8gScp06Nf/D+e9IfBNSWycSZPvMW0IPffXcMvfkXv3gOAmIh7QIVF8yLx6/49fE3mDiBnzY19Xu50QKqs4L+receMWeIuUSK51gh5EUk6ZNJIJOZVAIT85ZwtQKkgCoiIiKgiogIqCIiIgKqiIiIgCoiIqCKiIgIqCIiIgKqiIiAKiIiIqCKiAioIiIiAqqIiIiAKiIioIqIiAioIiIiAqqIiIAqIiIioIqIiAioIiICqoiIiIAqIiKgioiICKgiIiICqoiIgCoiIlKr/H8OxTF8rysySQAAAABJRU5ErkJggg=="
            }
        */
        /// <summary>
        /// Save image
        /// POST api/File/SaveImage
        /// </summary>
        /// <param name="jsonData">JSON data</param>
        /// <returns>Return the result</returns>
        [Route("SaveImage")]
        [HttpPost]
        public HttpResponseMessage SaveImage(JObject jsonData)
        {
            var msg = new FileModel();

            try
            {
                dynamic json = jsonData;
                string objectID = json.ObjectID;
                string image64 = json.Image64;

                var path = GetPathImage(AppSettings.ImageProfile);
                msg.URL = ZConverts.SaveImage(path, image64);
                msg.DateUpload = DateTime.Now;
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.ErrMsg = ex.Message;
            }

            return Response(msg);
        }

        #endregion
    }
}