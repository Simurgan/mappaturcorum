import Text from "@/views/components/text";
import "./style.scss";

const CollaborationsPage = () => {
  return (
    <section className="section collaborations-section">
      <div className="container">
        <Text tag="h1" fs={36} fw={500} lh={125} color="burgundy">
          Academic Collaborations
        </Text>
        <div className="collaborations-content">
          <div className="content-section">
            <Text tag="p" fs={16}>
              Here are the names helped our problems or made helpful suggestions
              for us (A-Z):
            </Text>
            <ul>
              {names.map((name) => (
                <li>
                  <Text tag="p" fs={16}>
                    {name}
                  </Text>
                </li>
              ))}
            </ul>
          </div>
        </div>
      </div>
    </section>
  );
};
export default CollaborationsPage;

const names = [
  "Ahmet Abdullah Saçmalı",
  "Akif Yerlioğlu",
  "Berat Açıl",
  "Cengiz Kırlı",
  "Çiğdem Kafescioğlu",
  "Derin Terzioğlu",
  "Elif Sezer Aydınlı",
  "Fatma Aladağ",
  "Fatma Öncel",
  "Güneş Işıksel",
  "Hale Eroğlu",
  "Hasan Umut",
  "İbrahim Kılıçaslan",
  "İpek Hüner",
  "İsmail Emre Pamuk",
  "Mehmet Ölmez",
  "Mehmet Şakir Yılmaz",
  "Muhammed Zahit Atçıl",
  "Muhammet Ali Orak",
  "Oya Pancaroğlu",
  "Özgür Kolçak",
  "Özgür Oral",
  "Paolo Girardelli",
  "Paolo Maranzana",
  "Sara Nur Yıldız",
  "Sevba Abdula",
  "Tülay Gençtürk Demircioğlu",
  "Yunus Uğur",
  "Zeynep Aydoğan",
  "Zeynep Oktay",
];
