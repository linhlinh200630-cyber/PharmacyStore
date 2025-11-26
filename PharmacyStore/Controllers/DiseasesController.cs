using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PharmacyStore.Controllers
{
    public class DiseasesController : Controller
    {
        // Model chứa thông tin bệnh
        public class DiseaseModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImageFile { get; set; } // Tên file ảnh (phải khớp với ảnh trong máy bạn)
            public string Overview { get; set; }  // Tổng quan
            public string Symptoms { get; set; }  // Triệu chứng
            public string Treatment { get; set; } // Cách điều trị
        }

        // DATABASE GIẢ LẬP (Dữ liệu đầy đủ cho 12 bệnh)
        private static readonly List<DiseaseModel> _data = new List<DiseaseModel>
        {
            // 1. Bệnh Phổi Tắc Nghẽn
            new DiseaseModel {
                Id = 1,
                Name = "Bệnh Phổi Tắc Nghẽn Mạn Tính (COPD)",
                ImageFile = "phoi.png",
                Overview = "Bệnh phổi tắc nghẽn mạn tính (COPD) là bệnh viêm phổi mãn tính gây tắc nghẽn luồng khí từ phổi. Bệnh thường do tiếp xúc lâu dài với các chất khí hoặc hạt kích thích, phổ biến nhất là khói thuốc lá.",
                Symptoms = "- Khó thở, đặc biệt là khi hoạt động thể chất.\n- Thở khò khè, tức ngực.\n- Ho mãn tính có thể tạo ra chất nhầy (đờm) màu trắng, vàng hoặc xanh lục.\n- Môi và móng tay xanh tím (do thiếu oxy).\n- Nhiễm trùng đường hô hấp thường xuyên.",
                Treatment = "- **Cai thuốc lá:** Là biện pháp quan trọng nhất để ngăn bệnh tiến triển.\n- **Dùng thuốc:** Các loại thuốc giãn phế quản, corticoid theo chỉ định của bác sĩ.\n- **Liệu pháp oxy:** Dùng cho trường hợp bệnh nặng.\n- **Tiêm vắc-xin:** Cúm và phế cầu khuẩn để tránh biến chứng."
            },

            // 2. Trào Ngược Dạ Dày
            new DiseaseModel {
                Id = 2,
                Name = "Trào Ngược Dạ Dày Thực Quản",
                ImageFile = "da day.jpg",
                Overview = "Trào ngược dạ dày thực quản (GERD) xảy ra khi axit dạ dày thường xuyên chảy ngược vào ống nối miệng và dạ dày (thực quản). Sự rửa ngược này có thể gây kích ứng niêm mạc thực quản.",
                Symptoms = "- Cảm giác nóng rát ở ngực (ợ nóng), thường sau khi ăn, tồi tệ hơn vào ban đêm.\n- Đau ngực.\n- Khó nuốt, cảm giác vướng ở cổ họng.\n- Ợ chua, buồn nôn.\n- Ho khan mãn tính hoặc khàn tiếng.",
                Treatment = "- **Thay đổi lối sống:** Chia nhỏ bữa ăn, không nằm ngay sau khi ăn, kê cao đầu giường khi ngủ.\n- **Chế độ ăn:** Tránh đồ cay nóng, rượu bia, cà phê, sô cô la.\n- **Dùng thuốc:** Thuốc kháng axit, thuốc ức chế bơm proton (PPI) theo đơn bác sĩ."
            },

            // 3. Bệnh Sởi
            new DiseaseModel {
                Id = 3,
                Name = "Bệnh Sởi",
                ImageFile = "soi.jpg",
                Overview = "Bệnh sởi là một bệnh truyền nhiễm cấp tính do virus gây ra, lây lan rất nhanh qua đường hô hấp. Bệnh có thể gây ra nhiều biến chứng nguy hiểm ở trẻ em và người chưa có miễn dịch.",
                Symptoms = "- Sốt cao, có thể lên tới 40 độ C.\n- Ho khan, chảy nước mũi, mắt đỏ (viêm kết mạc).\n- Xuất hiện các đốm trắng nhỏ bên trong miệng (đốm Koplik).\n- Phát ban đỏ lan từ mặt, cổ xuống toàn thân sau vài ngày sốt.",
                Treatment = "- Hiện chưa có thuốc đặc trị, chủ yếu là điều trị triệu chứng và hỗ trợ.\n- Hạ sốt bằng Paracetamol (không dùng Aspirin cho trẻ em).\n- Bổ sung Vitamin A liều cao để tránh biến chứng mắt.\n- Giữ vệ sinh thân thể, mắt, mũi họng.\n- **Phòng ngừa:** Tiêm vắc-xin Sởi là biện pháp hiệu quả nhất."
            },

            // 4. Đau Mắt Đỏ
            new DiseaseModel {
                Id = 4,
                Name = "Đau Mắt Đỏ (Viêm Kết Mạc)",
                ImageFile = "mat.jpg",
                Overview = "Đau mắt đỏ là tình trạng viêm màng trong suốt trên bề mặt nhãn cầu (kết mạc). Bệnh thường do virus, vi khuẩn hoặc dị ứng gây ra và rất dễ lây lan trong cộng đồng.",
                Symptoms = "- Mắt đỏ ngầu, cảm giác cộm như có cát trong mắt.\n- Ngứa mắt, chảy nước mắt nhiều.\n- Tiết nhiều ghèn (rỉ mắt) màu vàng hoặc xanh, thường dính chặt mi mắt khi ngủ dậy.\n- Nhạy cảm với ánh sáng.",
                Treatment = "- Rửa mắt thường xuyên bằng nước muối sinh lý (NaCl 0.9%).\n- Dùng thuốc nhỏ mắt kháng sinh (nếu do vi khuẩn) theo chỉ định.\n- Không dụi mắt, dùng riêng khăn mặt, vật dụng cá nhân.\n- Đeo kính râm để bảo vệ mắt và hạn chế lây lan."
            },

            // 5. Bệnh Cúm
            new DiseaseModel {
                Id = 5,
                Name = "Bệnh Cúm Mùa",
                ImageFile = "cum.jpg",
                Overview = "Cúm là bệnh nhiễm trùng đường hô hấp do virus cúm gây ra. Bệnh khác với cảm lạnh thông thường vì thường khởi phát đột ngột và triệu chứng nghiêm trọng hơn.",
                Symptoms = "- Sốt cao đột ngột, ớn lạnh.\n- Đau nhức cơ bắp dữ dội, mệt mỏi kiệt sức.\n- Ho khan, đau họng.\n- Đau đầu, ngạt mũi.",
                Treatment = "- Nghỉ ngơi tại giường, uống nhiều nước.\n- Dùng thuốc hạ sốt, giảm đau.\n- Có thể dùng thuốc kháng virus (Tamiflu) nếu được bác sĩ kê đơn sớm.\n- Tiêm vắc-xin cúm hàng năm để phòng ngừa."
            },

            // 6. Sốt Phát Ban
            new DiseaseModel {
                Id = 6,
                Name = "Sốt Phát Ban",
                ImageFile = "sot.jpg",
                Overview = "Sốt phát ban là bệnh thường gặp ở trẻ em, do virus (thường là virus Herpes 6 hoặc 7) gây ra. Bệnh đặc trưng bởi cơn sốt cao sau đó là nổi ban đỏ khắp người.",
                Symptoms = "- Sốt cao đột ngột (39-40 độ) kéo dài 3-5 ngày.\n- Sau khi hết sốt, các nốt ban màu hồng hoặc đỏ xuất hiện, bắt đầu từ ngực, bụng rồi lan ra tay chân.\n- Trẻ có thể quấy khóc, biếng ăn, sưng hạch bạch huyết.",
                Treatment = "- Không có thuốc đặc trị, bệnh thường tự khỏi sau 1 tuần.\n- Chườm ấm, dùng thuốc hạ sốt khi cần thiết.\n- Cho trẻ uống nhiều nước và bù điện giải.\n- Ăn thức ăn lỏng, dễ tiêu."
            },

            // 7. Bệnh Dị Ứng
            new DiseaseModel {
                Id = 7,
                Name = "Dị Ứng Thời Tiết / Mề Đay",
                ImageFile = "di ung.jpg",
                Overview = "Dị ứng là phản ứng quá mức của hệ miễn dịch đối với các tác nhân bên ngoài (phấn hoa, thời tiết, thức ăn...). Thời điểm giao mùa là lúc bệnh dễ bùng phát nhất.",
                Symptoms = "- Nổi mẩn đỏ, mề đay sần sùi trên da.\n- Ngứa ngáy dữ dội, càng gãi càng lan rộng.\n- Hắt hơi, sổ mũi, ngứa mắt (nếu dị ứng hô hấp).\n- Trường hợp nặng có thể gây khó thở, sưng phù mặt.",
                Treatment = "- **Tránh tác nhân:** Giữ ấm cơ thể khi trời lạnh, tránh tiếp xúc phấn hoa, bụi.\n- **Dùng thuốc:** Thuốc kháng Histamin để giảm ngứa và mẩn đỏ.\n- Bôi kem dưỡng ẩm để làm dịu da.\n- Đi khám ngay nếu có dấu hiệu khó thở."
            },

            // 8. Đau Cơ Xương Khớp
            new DiseaseModel {
                Id = 8,
                Name = "Đau Nhức Cơ Xương Khớp",
                ImageFile = "xuongkhop.jpg",
                Overview = "Đau nhức xương khớp thường gia tăng khi thời tiết thay đổi, đặc biệt là trời lạnh. Bệnh ảnh hưởng lớn đến khả năng vận động và chất lượng cuộc sống.",
                Symptoms = "- Đau nhức tại các khớp (gối, lưng, vai gáy).\n- Cứng khớp vào buổi sáng, khó cử động.\n- Khớp có thể sưng, nóng, đỏ.\n- Nghe tiếng lạo xạo khi di chuyển.",
                Treatment = "- Giữ ấm cơ thể, đặc biệt là các vùng khớp.\n- Chườm nóng để giãn cơ, giảm đau.\n- Tập thể dục nhẹ nhàng (đi bộ, yoga).\n- Dùng thuốc giảm đau, chống viêm theo chỉ định.\n- Bổ sung Canxi và Vitamin D."
            },

            // 9. Viêm Họng Cấp
            new DiseaseModel {
                Id = 9,
                Name = "Viêm Họng Cấp",
                ImageFile = "viemhong.jpg",
                Overview = "Viêm họng cấp là tình trạng viêm niêm mạc họng, thường xảy ra vào mùa lạnh. Nguyên nhân chủ yếu do virus (80%) hoặc vi khuẩn liên cầu.",
                Symptoms = "- Đau rát họng, nuốt vướng, nuốt đau.\n- Sốt cao, đau đầu, mệt mỏi.\n- Họng đỏ, amidan sưng to, có thể có mủ trắng.\n- Hạch cổ sưng đau.",
                Treatment = "- Súc miệng nước muối ấm thường xuyên.\n- Giữ ấm vùng cổ.\n- Uống nhiều nước ấm, ăn đồ mềm.\n- Dùng kháng sinh **chỉ khi** có chỉ định của bác sĩ (nếu do vi khuẩn).\n- Dùng viên ngậm hoặc siro ho để giảm triệu chứng."
            },

            // 10. Tay Chân Miệng
            new DiseaseModel {
                Id = 10,
                Name = "Bệnh Tay Chân Miệng",
                ImageFile = "taychanmieng.jpg",
                Overview = "Tay chân miệng là bệnh truyền nhiễm do virus đường ruột gây ra, lây lan rất nhanh ở trẻ nhỏ dưới 5 tuổi. Bệnh có thể gây biến chứng thần kinh nguy hiểm.",
                Symptoms = "- Sốt nhẹ hoặc cao.\n- Loét miệng: Các vết loét đỏ hay phỏng nước ở niêm mạc miệng, lợi, lưỡi gây đau khi ăn.\n- Phát ban dạng phỏng nước ở lòng bàn tay, lòng bàn chân, gối, mông.",
                Treatment = "- Hiện chưa có vắc-xin và thuốc đặc trị.\n- Chăm sóc tại nhà: Hạ sốt, giảm đau, vệ sinh răng miệng.\n- Cho trẻ ăn thức ăn nguội, mềm, lỏng.\n- **Theo dõi sát:** Nếu trẻ sốt cao không hạ, giật mình chới với, run tay chân thì phải đưa đi cấp cứu ngay."
            },

            // 11. Viêm Phế Quản
            new DiseaseModel {
                Id = 11,
                Name = "Viêm Phế Quản Cấp",
                ImageFile = "viemphequan.jpg",
                Overview = "Viêm phế quản là tình trạng viêm lớp niêm mạc của ống phế quản. Bệnh thường phát triển sau khi bị cảm lạnh hoặc cúm.",
                Symptoms = "- Ho nhiều, ho có đờm (đờm trong, trắng hoặc vàng lục).\n- Khó thở, thở khò khè.\n- Sốt nhẹ, ớn lạnh.\n- Tức ngực.",
                Treatment = "- Uống nhiều nước để làm loãng đờm.\n- Nghỉ ngơi đầy đủ.\n- Dùng thuốc long đờm hoặc giảm ho (tùy trường hợp).\n- Tránh khói thuốc và môi trường ô nhiễm.\n- Kháng sinh thường không cần thiết trừ khi nghi ngờ bội nhiễm vi khuẩn."
            },

            // 12. Viêm Xoang
            new DiseaseModel {
                Id = 12,
                Name = "Viêm Xoang",
                ImageFile = "viemxoang.jpg",
                Overview = "Viêm xoang là tình trạng các hốc xoang cạnh mũi bị viêm và sưng nề, gây ứ đọng dịch nhầy. Bệnh gây đau nhức khó chịu, đặc biệt khi thời tiết thay đổi.",
                Symptoms = "- Đau nhức vùng mặt (trán, má, giữa hai mắt).\n- Nghẹt mũi, chảy nước mũi (dịch vàng hoặc xanh).\n- Giảm khứu giác.\n- Ho, hơi thở có mùi hôi.",
                Treatment = "- Rửa mũi bằng nước muối sinh lý hàng ngày.\n- Xông hơi mũi bằng tinh dầu.\n- Dùng thuốc xịt mũi co mạch (không dùng quá 7 ngày).\n- Dùng kháng sinh và kháng viêm theo đơn nếu viêm xoang do vi khuẩn cấp tính."
            }
        };

        // GET: Diseases/Details/1
        public ActionResult Details(int id)
        {
            var disease = _data.FirstOrDefault(d => d.Id == id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }
    }
}