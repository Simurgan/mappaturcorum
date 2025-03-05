import Text from "@/views/components/text";
import "./style.scss";
import {CollaboratorType} from "@/models/collaborations.ts";

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
              {collaborators.map((collaborator) => (
                  <li key={collaborator.name}>
                    <img src={require("@/assets/icons/academia-logo.svg")} alt="Academia Logo" width="12" height="12"/>
                    {collaborator.academiaUrl !== "N/A" ? (
                        <a href={collaborator.academiaUrl} target="_blank" rel="noopener noreferrer"  className="collaborator-link">
                          <Text tag="p" fs={16}>
                            {collaborator.name}
                          </Text>
                        </a>
                    ) : (
                        <Text tag="p" fs={16}>{collaborator.name}</Text>
                    )}
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

const collaborators: CollaboratorType[] = [
  {name: "Ahmet Abdullah Saçmalı", academiaUrl: "https://durham.academia.edu/AhmetAbdullahSacmali"},
  {name: "Akif Yerlioğlu", academiaUrl: "https://boun.academia.edu/AkifYerlioglu"},
  {name: "Berat Açıl", academiaUrl: "https://boun.academia.edu/BeratA%C3%A7%C4%B1l"},
  {name: "Cengiz Kırlı", academiaUrl: "https://boun.academia.edu/CengizK%C4%B1rl%C4%B1"},
  {name: "Çiğdem Kafescioğlu", academiaUrl: "https://boun.academia.edu/cigdemkafescioglu"},
  {name: "Derin Terzioğlu", academiaUrl: "https://boun.academia.edu/DerinTerzioglu" },
  { name: "Elif Sezer Aydınlı", academiaUrl: "https://ku.academia.edu/ElifSezerAyd%C4%B1nl%C4%B1" },
  { name: "Fatma Aladağ", academiaUrl: "https://uni-leipzig.academia.edu/FatmaAladag" },
  { name: "Fatma Öncel", academiaUrl: "https://bahcesehir.academia.edu/Fatma%C3%96ncel" },
  { name: "Güneş Işıksel", academiaUrl: "https://medeniyet.academia.edu/G%C3%BCne%C5%9FI%C5%9F%C4%B1ksel" },
  { name: "Hale Eroğlu", academiaUrl: "https://bogaziciuniversity.academia.edu/ZeynebHaleErogluSager" },
  { name: "Hasan Umut", academiaUrl: "https://boun.academia.edu/HasanUmut" },
  { name: "İbrahim Kılıçaslan", academiaUrl: "N/A" }, // Eksik
  { name: "İpek Hüner", academiaUrl: "https://bogaziciuniversity.academia.edu/NIpekH%C3%BCnerCora" },
  { name: "İsmail Emre Pamuk", academiaUrl: "https://istanbul.academia.edu/%C4%B0smailEmrePamuk" },
  { name: "Mehmet Ölmez", academiaUrl: "N/A" }, // Eksik
  { name: "Mehmet Şakir Yılmaz", academiaUrl: "https://medeniyet.academia.edu/sakiryilmaz" },
  { name: "Muhammed Zahit Atçıl", academiaUrl: "https://boun.academia.edu/ZahitAt%C3%A7%C4%B1l" },
  { name: "Muhammet Ali Orak", academiaUrl: "N/A" }, // Eksik
  { name: "Oya Pancaroğlu", academiaUrl: "https://boun.academia.edu/OyaPancaro%C4%9Flu" },
  { name: "Özgür Kolçak", academiaUrl: "https://istanbul.academia.edu/%C3%96zg%C3%BCrKol%C3%A7ak" },
  { name: "Özgür Oral", academiaUrl: "https://istanbul.academia.edu/%C3%96zg%C3%BCrOral" },
  { name: "Paolo Girardelli", academiaUrl: "https://boun.academia.edu/PaoloGirardelli" },
  { name: "Paolo Maranzana", academiaUrl: "https://bogaziciuniversity.academia.edu/PaoloMaranzana" },
  { name: "Sara Nur Yıldız", academiaUrl: "https://metu.academia.edu/SaraNurYildiz" },
  { name: "Sevba Abdula", academiaUrl: "https://marmara.academia.edu/sevbaabdula" },
  { name: "Tülay Gençtürk Demircioğlu", academiaUrl: "https://boun.academia.edu/tülaygençtürk" },
  { name: "Yunus Uğur", academiaUrl: "https://marmara.academia.edu/YunusUgur" },
  { name: "Zeynep Aydoğan", academiaUrl: "https://forth.academia.edu/ZeynepAydogan" },
  { name: "Zeynep Oktay", academiaUrl: "https://boun.academia.edu/ZeynepOktay" }
];
