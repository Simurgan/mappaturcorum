import { Team } from "@/models/members";

export const Teams: Team[] = [
  {
    name: "Project Team",
    subteams: [
      {
        name: "Project Founder",
        members: [
          {
            name: "Muzaffer Çakır",
            descs: ["Boğaziçi University", "Undergraduate History", "Student"],
            image: require("@/assets/images/member-images/muzaffer.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/muzaffer-%C3%A7ak%C4%B1r-7897a026b/",
              },
              { type: "email", url: "muzaffercakir94@gmail.com" },
            ],
          },
        ],
      },
      {
        name: "Coordinators",
        members: [
          {
            name: "Emirhan Kuşaksız",
            descs: ["Boğaziçi University", "Undergraduate History", "Student"],
            image: require("@/assets/images/member-images/emirhan.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/emirhan-ku%C5%9Faks%C4%B1z-328962292/",
              },
              { type: "email", url: "emirhankusaksiz@gmail.com" },
            ],
          },
          {
            name: "Mehmet Akif Top",
            descs: ["İstanbul University", "Undergraduate History", "Student"],
            image: require("@/assets/images/member-images/akif.png"),
            socials: [
              { type: "linkedin", url: "https://www.linkedin.com/in/akfmttp/" },
              { type: "email", url: "akfmttp@gmail.com" },
            ],
          },
        ],
      },
      {
        name: "Researchers",
        members: [
          {
            name: "İsmail Demirtaş",
            descs: ["Boğaziçi University", "Undergraduate History", "Student"],
            image: require("@/assets/images/member-images/ismail.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/ismail-zahid-demirta%C5%9F-2545a0353/?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=android_app",
              },
              { type: "email", url: "izdemirtas751@gmail.com" },
            ],
          },
          {
            name: "Gökçe Yılmaz",
            descs: [
              "Boğaziçi University Undergraduate",
              "History & Turkish Language and Literature",
              "Double Major Student",
            ],
            image: require("@/assets/images/member-images/gokce.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/gokcenuryilmaz/",
              },
              { type: "email", url: "ylmzgokce2000@gmail.com" },
            ],
          },
          {
            name: "Şeyma Sarı",
            descs: ["Boğaziçi University", "Graduate History", "Student"],
            image: require("@/assets/images/member-images/seyma.png"),
            socials: [
              {
                type: "twitter",
                url: "https://x.com/SeymaSar8/status/1857832565001871761",
              },
              { type: "email", url: "seymasari79@gmail.com" },
            ],
          },
        ],
      },
    ],
  },
  {
    name: "Technical Team",
    subteams: [
      {
        name: "Developers",
        members: [
          {
            name: "Ömer Şükrü Uyduran",
            descs: [
              "Boğaziçi University Undergraduate",
              "Computer Engineering",
              "Student",
            ],
            image: require("@/assets/images/member-images/omar.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/uyduranomar/",
              },
              { type: "email", url: "uyduranomersukru@gmail.com" },
              { type: "github", url: "https://github.com/Simurgan" },
            ],
          },
          {
            name: "Said Yolcu",
            descs: ["Boğaziçi University", "Computer Engineering", "Graduate"],
            image: require("@/assets/images/member-images/said.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/mehmet-said-yolcu-a1aa41232/",
              },
            ],
          },
          {
            name: "Burak Kızılay",
            descs: [
              "Manisa Celal Bayar University",
              "Graduate Software Engineering",
              "Student",
            ],
            image: require("@/assets/images/member-images/burak.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/burak-kizilay/",
              },
            ],
          },
        ],
      },
      {
        name: "Designers",
        members: [
          {
            name: "Seher Doğan",
            descs: [
              "Hacettepe University",
              "Undergraduate Graphic Design",
              "Student",
            ],
            image: require("@/assets/images/member-images/seher.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/seher-do%C4%9Fan-951ab0235/",
              },
            ],
          },
        ],
      },
    ],
  },
];
