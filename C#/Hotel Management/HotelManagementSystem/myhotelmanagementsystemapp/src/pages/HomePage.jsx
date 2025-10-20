// pages/HomePage.jsx
import Hero from "../sections/HeroSection/Hero";
import AboutUs from "../sections/AboutUsSection/AboutUs";
import SubHero from "../sections/SubHeroSection/SubHero";
import RoomsSection from "../sections/RoomsSection/Rooms";
import MenuSection from "../sections/MenuSection/Menu";
import Footer from "../sections/FooterSection/Footer";
import Header from "../sections/HeaderSection/Header";

export default function HomePage() {
    return (
        <>
            <Header />
            <Hero />
            <AboutUs />
            <SubHero />
            <RoomsSection /> {/* Homepage version */}
            <MenuSection />
            <Footer />
        </>
    );
}
