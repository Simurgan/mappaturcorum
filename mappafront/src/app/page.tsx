import Text from "@/views/components/text";

export default function Home() {
  return (
    <section className="section home-section">
      <div className="container">
        <Text
          tag="h1"
          fw="900"
          lgfw="700"
          mdfw="500"
          smfw="400"
          fs="40"
          lgfs="36"
          mdfs="32"
          smfs="28"
          lh="14"
          color="burgundy"
          isCenter={true}
        >
          Hello mf!
        </Text>
      </div>
    </section>
  );
}
