using Emergence.Model;
using Emergence.Model.Equipment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
{
    public class Barracks : INotifyPropertyChanged
    {
        private ObservableCollection<Background> availableBackgrounds;
        public ObservableCollection<Background> AvailableBackgrounds
        {
            get
            {
                return availableBackgrounds;
            }
            private set
            {
                availableBackgrounds = value;
                NotifyPropertyChanged("AvailableBackgrounds");
            }
        }

        private ObservableCollection<Lifestyle> availableLifestyles;
        public ObservableCollection<Lifestyle> AvailableLifestyles
        {
            get
            {
                return availableLifestyles;
            }
            private set
            {
                availableLifestyles = value;
                NotifyPropertyChanged("AvailableLifestyles");
            }
        }

        private ObservableCollection<RacialTrait> availableRacialTraits;
        public ObservableCollection<RacialTrait> AvailableRacialTraits
        {
            get
            {
                return availableRacialTraits;
            }
            private set
            {
                availableRacialTraits = value;
                NotifyPropertyChanged("AvailableRacialTraits");
            }
        }

        public Barracks()
        {
            InitializeBackgrounds();
            InitializeRacialTraits();
        }

        private void InitializeBackgrounds()
        {
            #region Lifestyles of the rich and famous

            Lifestyle Poor = new Lifestyle();
            Lifestyle Low = new Lifestyle();
            Lifestyle Middle = new Lifestyle();
            Lifestyle High = new Lifestyle();
            Lifestyle Extravagant = new Lifestyle();

            Poor.Name = "Poor";
            Poor.Description = "You live a meager life. You eat small quantities of low-quality food. If you even have a home, it is small and in an impoverished area. When traveling or adventuring, you likely eat only basic rations and sleep outside or in your beat-up car.You wear clothing that is often salvaged or made from salvaged materials. You have no money available for medications, vitamins, or supplements.Examples of Poor Lifestyle homes include a van, a shanty in the woods, a bed in a hostel, a studio apartment with a communal bathroom, and a low-income government housing group.";
            Low.Name = "Low";
            Low.Description = "You live a little better than the poorest adventurers. You usually have enough to eat, but the quality of your food still leaves a lot to be desired. You likely have a private apartment with its own bathroom, but it is small and in an undesirable area. Your clothing is of low quality but was purchased new and is probably fairly clean. You can afford cheap medications when necessary and possibly vitamins if desired. When traveling or adventuring, you can afford to eat at restaurants occasionally and sleep in hotels with a similar frequency. When you do so, you gravitate toward the most inexpensive food and lodging available. Examples of Low Lifestyle homes include a 1 bedroom apartment, a tiny cottage in the country, or a bed in a small but adequate workshop.";
            Middle.Name = "Middle";
            Middle.Description = "Your Lifestyle is in line with the average citizen’s. You have plenty of food, wear decent clothing with a couple fancier outfits set aside for special occasions, and live in a comfortable home. You have access to most medicines and supplements, doctor visits if necessary, and your home and clothes are kept clean. When traveling or adventuring, you can eat and sleep in reasonably-priced locations when they are available without significantly impacting your budget. Examples of Middle Lifestyle homes include a nice multi-room apartment, a modest but well-kept suburban house, or a workshop with an attached apartment.";
            High.Name = "High";
            High.Description = "High: You enjoy a life afforded to few. You have access to more food and drink than you can consume, and the quality is higher than most eat on special occasions. Your home is unnecessarily spacious in addition to being in an upper-class neighborhood. Most of your clothing is high-quality. You can visit doctors and obtain medications, supplements, and treatments without concern to your budget. You have access to cleaning, cooking, and maintenance services that increase the quality of your home life. Your home is likely well protected by a security service, geography, internal security countermeasures, or the like. When traveling, you can eat and drink whatever you would like and sleep at four and five-star hotels. It is also in your budget to occasionally pick up the tab for a group, allowing those in your party who have lower Lifestyles to enjoy some occasional creature comforts. Examples of High Lifestyle homes include a luxury loft, a spacious suburban home, a Night Wood treetop residence, or a small warehouse with a handsomely decorated apartment.";
            Extravagant.Name = "Extravagant";
            Extravagant.Description = "You’re part of modern aristocracy. You wear the highest quality clothes and replace them more often than need dictates. You eat excellent food, which is available at all times. When at home you eat at restaurants or have someone cook for you most of the time. Doctors, specialists, experimental treatments, and medications are all available at your whim. Your home can be a fortress if you so desire; personal guards, Obsidian Blade contracts, internal countermeasures, and secret passages are all available to you. When traveling, you seek out the best of the best of everything, eating and drinking the finest fare of each area you pass through. You can regularly pay the tab for dinners and lodging for comrades without damaging your budget. Examples of Extravagant Lifestyle homes include a spectacular upper-floor penthouse with a view of the city, an impressive home in the most well-off of suburbs, a warehouse with a fantastically spacious loft, and whatever else you can imagine that could easily house 6-8 people.";

            availableLifestyles = new ObservableCollection<Lifestyle>();
            availableLifestyles.Add(Poor);
            availableLifestyles.Add(Low);
            availableLifestyles.Add(Middle);
            availableLifestyles.Add(High);
            availableLifestyles.Add(Extravagant);

            #endregion

            #region Background special sacuce
            BackgroundSpecial CombatVet = new BackgroundSpecial();
            BackgroundSpecial CustomBuildPoints = new BackgroundSpecial();
            BackgroundSpecial DiplomaticImmunity = new BackgroundSpecial();
            BackgroundSpecial Dicipline = new BackgroundSpecial();
            BackgroundSpecial Kinship = new BackgroundSpecial();
            BackgroundSpecial Mentor = new BackgroundSpecial();
            BackgroundSpecial NameDrop = new BackgroundSpecial();
            BackgroundSpecial Polyglot = new BackgroundSpecial();
            BackgroundSpecial RapSheet = new BackgroundSpecial();
            BackgroundSpecial Requisition = new BackgroundSpecial();
            BackgroundSpecial Research = new BackgroundSpecial();
            BackgroundSpecial Scarred = new BackgroundSpecial();
            BackgroundSpecial ServiceBenefits = new BackgroundSpecial();
            BackgroundSpecial Vagrant = new BackgroundSpecial();
            BackgroundSpecial WorkshopAccess = new BackgroundSpecial();
            BackgroundSpecial Worldly = new BackgroundSpecial();

            CombatVet.Name = "Combat Veteran";
            CombatVet.Description = "Gain Combat Sign for free.";
            CustomBuildPoints.Name = "Custom Build Point Allotment";
            CustomBuildPoints.Description = "Gain 90 Build Points.";
            DiplomaticImmunity.Name = "Diplomatic Immunity";
            DiplomaticImmunity.Description = "You will always be given safe haven in any facility affliated with the chosen government and can arrange safe passage back home if needed.";
            Dicipline.Name = "Dicipline";
            Dicipline.Description = "Gain a +1 to Resolve Defense and Resistance Checks; suffer a -2 to Resolve Defense and Resistance Checks for 1 week if you violate your chosen code of conduct. The codes of conduct are: • Peace: Do no harm in action or inaction • Kindness: Help all those you have the power to help • Vengeance: Exact revenge for any and all infractions • Worship: Do all you can to uphold the edicts of the being you worship • Strength: Never give up, never surrender";
            Kinship.Name = "Kinship";
            Kinship.Description = "You can call on your tribe for assistance. When making Connection Checks related to your tribe, you can add your highest Attribute to the roll. Whether a Connection Check is related to your tribe is the GM’s discretion.";
            Mentor.Name = "Mentor";
            Mentor.Description = "Use a Connection Check to call on a mentor for appropriate favors. When making this Check, add your highest Attribute to the roll. Whether the mentor has the power to help your with the request is the GM’s discretion and based on the abilities that mentor is assumed to have.";
            NameDrop.Name = "Name Drop";
            NameDrop.Description = "You can spend 1 Spirit to dig up a lead or get out of trouble. This ability can give you a hint when Connection Checks and other means have failed. For example, you may be able to free yourself from interrogation or law enforcement. This ability will not allow you or your allies to get away with murder, assault, or any other blatant criminal behavior. The ability is intended to include situations of property damage, self - defense, and misunderstandings.";
            Polyglot.Name = "Polyglot";
            Polyglot.Description = "Gain an additional language for free.";
            RapSheet.Name = "Rap Sheet";
            RapSheet.Description = "You have been around the block and seen the darker side of life. You retain the Street Cred of your starting Lifestyle regardless of your current Lifestyle.";
            Requisition.Name = "Requisition";
            Requisition.Description = "With 1-hour notice, you can obtain up to 100x your Connection Check result worth of equipment. This equipment should be appropriate to your Background. You gain a +5 to this roll if you are currently on a mission from any organization with which you have positive Notoriety and a cumulative -1 to the Connection Check each time you use this ability within 30 days.";
            Research.Name = "Research";
            Research.Description = "You can make any Knowledge Skill Check as a Trained Skill Check with value 3 + Focus. This ability requires at least 1 hour and access to the Internet or other appropriate research resources.";
            Scarred.Name = "Scarred";
            Scarred.Description = "Gain +1 to Long-Term Recovery and all Healing you receive.";
            ServiceBenefits.Name = "Service Benefits";
            ServiceBenefits.Description = "While inside a facility controlled by or allied with any organization with which you have positive Notoriety, you can receive free medical assistance to stabilize and recover from wounds (provided the facility has the means). This ability will only allow for the recovery of lost HP; if specialty Healing or Augmentation work is required, it will be performed at your expense.";
            Vagrant.Name = "Vagrant";
            Vagrant.Description = "There are almost no physical records of you. A -4 will be applied to any Checks to gather information about you through computer or records searching. Only word-of-mouth investigation avoids this penalty. As long as you have a Poor or Low Lifestyle, you can relocate to an equal Lifestyle dwelling at no cost with 24 hours’ notice.Gain a +1 to Long-Term Recovery while you have Poor Lifestyle.";
            WorkshopAccess.Name = "Workshop Access";
            WorkshopAccess.Description = "You have access to a full workshop for up to 2 Trade Skills.";
            Worldly.Name = "Worldly";
            Worldly.Description = "You can add your highest ability score to Connection Checks to locate and obtain equipment and to Knowledge Checks pertaining to travel, borders, and customs.";
            #endregion

            #region Backgrounds

            Background Agent = new Background();
            Background Apprentice = new Background();
            Background Athlete = new Background();
            Background BlueCollar = new Background();
            Background Crimnial = new Background();
            Background Custom = new Background();
            Background Dilettante = new Background();
            Background Diplomat = new Background();
            Background Educated = new Background();
            Background LawEnforcement = new Background();
            Background Mercenary = new Background();
            Background Monastic = new Background();
            Background Officer = new Background();
            Background Soldier = new Background();
            Background Street = new Background();
            Background Tribal = new Background();

            Agent.Name = "Agent";
            Agent.Description = "You serve as the agent of a government or another established group. Your primary role has been to e ciently execute the will of your superiors.";
            Agent.StartingArray = new AttributeArray(1, 1, 4, 1, 1, 1, 4, 3, 1);
            Agent.Skills = "Athletics 2, Close Combat 2, Deception 3, Negotiation 2, Shortarms 2, 8 BP toward any Knowledge Skill";
            Agent.TalentTraining = "None";
            Agent.Notoriety = "Government +1 or Group +1, Rival -1";
            Agent.Contacts = new List<Contact>();
            Agent.Contacts.Add(new Contact(EquipmentType.Gear));
            Agent.StartingCash = 15000;
            Agent.StartingLifestyle = High;
            Agent.SpecialProperties = new List<BackgroundSpecial>();
            Agent.SpecialProperties.Add(Polyglot);
            Agent.SpecialProperties.Add(Requisition);

            Apprentice.Name = "Apprentice";
            Apprentice.Description = "You have been studying under a mentor of exceptional skill.";
            Apprentice.StartingArray = new AttributeArray(1, 4, 1, 1, 4, 3, 1, 1, 1);
            Apprentice.Skills = "30 BP toward any skills";
            Apprentice.TalentTraining = "20 BP toward any talents";
            Apprentice.Notoriety = "None";
            Apprentice.Contacts = new List<Contact>();
            Apprentice.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Apprentice.StartingCash = 8000;
            Apprentice.StartingLifestyle = Middle;
            Apprentice.SpecialProperties = new List<BackgroundSpecial>();
            Apprentice.SpecialProperties.Add(Mentor);

            Athlete.Name = "Athlete";
            Athlete.Description = "You played collegiate sports but never made it to the big leagues.";
            Athlete.StartingArray = new AttributeArray(1, 4, 4, 3, 1, 1, 1, 1, 1);
            Athlete.Skills = "Athletics 4, Unarmed 2, Thrown 3";
            Athlete.TalentTraining = "30 BP among Quickness, Toughness, and Armored Fighting Talent Trees";
            Athlete.Notoriety = "Local +1, Rival -1";
            Athlete.Contacts = new List<Contact>();
            Athlete.StartingCash = 3000;
            Athlete.StartingLifestyle = Low;
            Athlete.SpecialProperties = new List<BackgroundSpecial>();
            Athlete.SpecialProperties.Add(Scarred);

            BlueCollar.Name = "Blue Collar";
            BlueCollar.Description = "You have been working a blue collar job for years and have become quite skilled.";
            BlueCollar.StartingArray = new AttributeArray(1, 4, 1, 4, 1, 1, 1, 3, 1);
            BlueCollar.Skills = "Any Combat Skill 3, 30 BP toward any Specialized Skill(s)";
            BlueCollar.TalentTraining = "15 BP toward a Talent Tree of your choice";
            BlueCollar.Notoriety = "Union +1, Group -1";
            BlueCollar.Contacts = new List<Contact>();
            BlueCollar.Contacts.Add(new Contact(EquipmentType.Gear));
            BlueCollar.Contacts.Add(new Contact(EquipmentType.Lifestyle));
            BlueCollar.StartingCash = 4000;
            BlueCollar.StartingLifestyle = Middle;
            BlueCollar.SpecialProperties = new List<BackgroundSpecial>();
            BlueCollar.SpecialProperties.Add(WorkshopAccess);

            Crimnial.Name = "Criminal";
            Crimnial.Description = "The law is something that has gotten in your way many times in the past. You’re familiar with the local law enforcement. Perhaps you are a member of the local crime syndicate, thieves’ guild, or gang.";
            Crimnial.StartingArray = new AttributeArray(1, 1, 4, 4, 1, 1, 1, 3, 1);
            Crimnial.Skills = "Intimidate 3, Thievery 3, Pilot Land 2, Shortarms 3, Stealth 3";
            Crimnial.TalentTraining = "15 BP between Gunfighting and Automatics";
            Crimnial.Notoriety = "Criminal +2, Law -1, Local -1";
            Crimnial.Contacts = new List<Contact>();
            Crimnial.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Crimnial.StartingCash = 5000;
            Crimnial.StartingLifestyle = Low;
            Crimnial.SpecialProperties = new List<BackgroundSpecial>();
            Crimnial.SpecialProperties.Add(RapSheet);

            Custom.Name = "Custom";
            Custom.Description = "Your history is either shrouded in mystery or very different than most adventurers on Kython.";
            Custom.StartingArray = new AttributeArray(1, 1, 1, 1, 1, 1, 1, 1, 1);
            Custom.Skills = "None";
            Custom.TalentTraining = "None";
            Custom.Notoriety = "None";
            Custom.Contacts = new List<Contact>();
            Custom.StartingCash = 0;
            Custom.StartingLifestyle = Low;
            Custom.SpecialProperties = new List<BackgroundSpecial>();
            Custom.SpecialProperties.Add(CustomBuildPoints);

            Dilettante.Name = "Dilettante";
            Dilettante.Description = "An upper class upbringing, surrounded by the trappings of wealth.";
            Dilettante.StartingArray = new AttributeArray(1, 1, 1, 1, 4, 1, 4, 1, 3);
            Dilettante.Skills = "Persuasion 2, Negotiation 2, Athletics 3";
            Dilettante.TalentTraining = "15 BP toward Leadership Talent Tree";
            Dilettante.Notoriety = "None";
            Dilettante.Contacts = new List<Contact>();
            Dilettante.StartingCash = 20000;
            Dilettante.StartingLifestyle = High;
            Dilettante.SpecialProperties = new List<BackgroundSpecial>();
            Dilettante.SpecialProperties.Add(Polyglot);

            Diplomat.Name = "Diplomat";
            Diplomat.Description = "You have spent years developing governmental relationships and honed your Skills in interpersonal affairs.";
            Diplomat.StartingArray = new AttributeArray(1, 1, 1, 1, 3, 4, 4, 1, 1);
            Diplomat.Skills = "Deception 3, Negotiation 3, Persuasion 3";
            Diplomat.TalentTraining = "15 BP toward Leadership Talent Tree";
            Diplomat.Notoriety = "Government +1, 2nd Government +1, Rival -1, 2nd Rival -1";
            Diplomat.Contacts = new List<Contact>();
            Diplomat.Contacts.Add(new Contact(EquipmentType.Gear));
            Diplomat.StartingCash = 10000;
            Diplomat.StartingLifestyle = High;
            Diplomat.SpecialProperties = new List<BackgroundSpecial>();
            Diplomat.SpecialProperties.Add(DiplomaticImmunity);
            Diplomat.SpecialProperties.Add(Polyglot);
            Diplomat.SpecialProperties.Add(Polyglot);
            Diplomat.SpecialProperties.Add(Polyglot);

            Educated.Name = "Educated";
            Educated.Description = "You have advanced degrees in one or more areas of study.";
            Educated.StartingArray = new AttributeArray(1, 1, 1, 1, 4, 4, 1, 1, 3);
            Educated.Skills = "10 BP toward Knowledge Skill(s), 40 BP toward Specialized Skill(s)";
            Educated.TalentTraining = "None";
            Educated.Notoriety = "Community +1 (must correspond to the university you attended)";
            Educated.Contacts = new List<Contact>();
            Educated.Contacts.Add(new Contact(EquipmentType.Augment));
            Educated.StartingCash = 7000;
            Educated.StartingLifestyle = High;
            Educated.SpecialProperties = new List<BackgroundSpecial>();
            Educated.SpecialProperties.Add(Research);
            Educated.SpecialProperties.Add(Polyglot);

            LawEnforcement.Name = "Law Enforcement";
            LawEnforcement.Description = "You have combat training and connections within the local area.";
            LawEnforcement.StartingArray = new AttributeArray(1, 1, 1, 4, 1, 1, 4, 1, 3);
            LawEnforcement.Skills = "Persuasion 3, Shortarms 3, Unarmed 2, Pilot Land 3";
            LawEnforcement.TalentTraining = "15 BP toward Grappling Talent Tree";
            LawEnforcement.Notoriety = "Criminal -1, 2nd Criminal -1, Law +1, Local+1";
            LawEnforcement.Contacts = new List<Contact>();
            LawEnforcement.Contacts.Add(new Contact(EquipmentType.WeaponOrArmor));
            LawEnforcement.StartingCash = 9000;
            LawEnforcement.StartingLifestyle = Middle;
            LawEnforcement.SpecialProperties = new List<BackgroundSpecial>();
            LawEnforcement.SpecialProperties.Add(NameDrop);
            LawEnforcement.SpecialProperties.Add(CombatVet);

            Mercenary.Name = "Mercenary";
            Mercenary.Description = "You work as hired muscle and have spent years honing this trade.";
            Mercenary.StartingArray = new AttributeArray(1, 1, 1, 4, 4, 1, 1, 1, 3);
            Mercenary.Skills = "Interrogation 3, any 2 Combat Skills at 2, Negotiation 3";
            Mercenary.TalentTraining = "20 BP among Toughness Talent Tree and/or any Combat Talent Tree";
            Mercenary.Notoriety = "Local +1";
            Mercenary.Contacts = new List<Contact>();
            Mercenary.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Mercenary.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Mercenary.StartingCash = 10000;
            Mercenary.StartingLifestyle = Low;
            Mercenary.SpecialProperties = new List<BackgroundSpecial>();
            Mercenary.SpecialProperties.Add(Worldly);
            Mercenary.SpecialProperties.Add(CombatVet);

            Monastic.Name = "Monastic";
            Monastic.Description = "You’ve been living in isolation with a like - minded community. You follow a strict regimen and have a code of conduct.";
            Monastic.StartingArray = new AttributeArray(1, 1, 3, 4, 1, 4, 1, 1, 1);
            Monastic.Skills = "Any 2 Combat Skills at 2, First Aid 3, Knowledge Geography 3";
            Monastic.TalentTraining = "35 BP among Martial Arts, Spellcasting, and/or Grappling Talent Trees";
            Monastic.Notoriety = "Religion +1, Religion -1";
            Monastic.Contacts = new List<Contact>();
            Monastic.Contacts.Add(new Contact(EquipmentType.AmplifierOrWeapon));
            Monastic.StartingCash = 1000;
            Monastic.StartingLifestyle = Poor;
            Monastic.SpecialProperties = new List<BackgroundSpecial>();
            Monastic.SpecialProperties.Add(Dicipline);

            Officer.Name = "Officer";
            Officer.Description = "You were a military offcer. You commanded troops in several engagements and have combat training as well as leadership experience.";
            Officer.StartingArray = new AttributeArray(1, 4, 1, 3, 1, 1, 4, 1, 1);
            Officer.Skills = "Athletics 3, Close Combat 1, Persuasion 3, Negotiation 2, Longarms 2, Shortarms 2";
            Officer.TalentTraining = "15 BP toward Leadership Talent Tree";
            Officer.Notoriety = "Government +1, Local +1, Rival -2";
            Officer.Contacts = new List<Contact>();
            Officer.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Officer.StartingCash = 5000;
            Officer.StartingLifestyle = Middle;
            Officer.SpecialProperties = new List<BackgroundSpecial>();
            Officer.SpecialProperties.Add(ServiceBenefits);
            Officer.SpecialProperties.Add(CombatVet);

            Soldier.Name = "Soldier";
            Soldier.Description = "You were a member of a front line fighting force.";
            Soldier.StartingArray = new AttributeArray(1, 13, 4, 1, 4, 1, 1, 1, 1);
            Soldier.Skills = "Athletics 4, Close Combat 2, Longarms 2, Shortarms 2";
            Soldier.TalentTraining = "15 BP among Armored Fighting and/or Toughness Talent Trees";
            Soldier.Notoriety = "Local +1, Local -1";
            Soldier.Contacts = new List<Contact>();
            Soldier.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Soldier.StartingCash = 6000;
            Soldier.StartingLifestyle = Middle;
            Soldier.SpecialProperties = new List<BackgroundSpecial>();
            Soldier.SpecialProperties.Add(ServiceBenefits);
            Soldier.SpecialProperties.Add(CombatVet);

            Street.Name = "Street";
            Street.Description = "You are from a low-class upbringing. It seems like every law and regulation is meant to prevent you from rising from your position. Perhaps you are still on the streets or have managed against the odds, but your past still follows you.";
            Street.StartingArray = new AttributeArray(1, 1, 4, 1, 1, 4, 1, 3, 1);
            Street.Skills = "Close Combat, Unarmed or Dueling 3, Stealth 3, Survival 3";
            Street.TalentTraining = "15 BP toward Toughness Talent Tree, 15 BP among Brawling, Grappling, and / or Brutality Talent Trees";
            Street.Notoriety = "Criminal +1, 2nd Criminal +1, Law -1, Local-1";
            Street.Contacts = new List<Contact>();
            Street.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Street.Contacts.Add(new Contact(EquipmentType.PickAnyOne));
            Street.StartingCash = 2000;
            Street.StartingLifestyle = Low;
            Street.SpecialProperties = new List<BackgroundSpecial>();
            Street.SpecialProperties.Add(Vagrant);

            Tribal.Name = "Tribal";
            Tribal.Description = "You have come from a tribal community. This tight-knit community is very self-suffcient, and those traits have rubbed off on you.";
            Tribal.StartingArray = new AttributeArray(1, 1, 4, 1, 1, 4, 1, 3, 1);
            Tribal.Skills = "Survival 3, any Combat Skill 3, Specialized Skill 3, 2nd Specialized Skill 3 (choose only from Artistry, Enchanting, Healing, Husbandry, or Smithing)";
            Tribal.TalentTraining = "15 BP toward any Talent Tree linked to a combat Skill";
            Tribal.Notoriety = "Community +1";
            Tribal.Contacts = new List<Contact>();
            Tribal.Contacts.Add(new Contact(EquipmentType.AmplifierOrWeaponOrAugment));
            Tribal.StartingCash = 4000;
            Tribal.StartingLifestyle = Low;
            Tribal.SpecialProperties = new List<BackgroundSpecial>();
            Tribal.SpecialProperties.Add(Kinship);

            availableBackgrounds = new ObservableCollection<Background>();
            availableBackgrounds.Add(Agent);
            availableBackgrounds.Add(Apprentice);
            availableBackgrounds.Add(Athlete);
            availableBackgrounds.Add(BlueCollar);
            availableBackgrounds.Add(Crimnial);
            availableBackgrounds.Add(Custom);
            availableBackgrounds.Add(Dilettante);
            availableBackgrounds.Add(Diplomat);
            availableBackgrounds.Add(Educated);
            availableBackgrounds.Add(LawEnforcement);
            availableBackgrounds.Add(Mercenary);
            availableBackgrounds.Add(Monastic);
            availableBackgrounds.Add(Officer);
            availableBackgrounds.Add(Soldier);
            availableBackgrounds.Add(Street);
            availableBackgrounds.Add(Tribal);

            #endregion
        }

        private void InitializeRacialTraits()
        {
            RacialTrait Adaptable = new RacialTrait();
            RacialTrait Affinity1 = new RacialTrait();
            RacialTrait Affinity2 = new RacialTrait();
            RacialTrait Affinity3 = new RacialTrait();
            RacialTrait Affinity4 = new RacialTrait();
            RacialTrait Affinity5 = new RacialTrait();
            RacialTrait Affinity6 = new RacialTrait();
            RacialTrait Affinity7 = new RacialTrait();
            RacialTrait Affinity8 = new RacialTrait();
            RacialTrait Affinity9 = new RacialTrait();
            RacialTrait Affinity10 = new RacialTrait();
            RacialTrait Affinity11 = new RacialTrait();
            RacialTrait Affinity12 = new RacialTrait();
            RacialTrait Affinity13 = new RacialTrait();
            RacialTrait Affinity14 = new RacialTrait();
            RacialTrait Affinity15 = new RacialTrait();
            RacialTrait Affinity16 = new RacialTrait();
            RacialTrait Affinity17 = new RacialTrait();
            RacialTrait Affinity18 = new RacialTrait();
            RacialTrait Affinity19 = new RacialTrait();
            RacialTrait Affinity20 = new RacialTrait();
            RacialTrait Affinity21 = new RacialTrait();
            RacialTrait Affinity22 = new RacialTrait();
            RacialTrait Affinity23 = new RacialTrait();
            RacialTrait Affinity24 = new RacialTrait();
            RacialTrait Affinity25 = new RacialTrait();
            RacialTrait Affinity26 = new RacialTrait();
            RacialTrait Affinity27 = new RacialTrait();
            RacialTrait Affinity28 = new RacialTrait();
            RacialTrait Affinity29 = new RacialTrait();
            RacialTrait Affinity30 = new RacialTrait();
            RacialTrait Affinity31 = new RacialTrait();
            RacialTrait Affinity32 = new RacialTrait();
            RacialTrait Affinity33 = new RacialTrait();
            RacialTrait Affinity34 = new RacialTrait();
            RacialTrait Animalistic = new RacialTrait();
            RacialTrait ArcaneHeritage = new RacialTrait();
            RacialTrait ArcaneElite = new RacialTrait();
            RacialTrait BeastWithin = new RacialTrait();
            RacialTrait Connected = new RacialTrait();
            RacialTrait Earthborn = new RacialTrait();
            RacialTrait EarthbornNoble = new RacialTrait();
            RacialTrait Educated = new RacialTrait();
            RacialTrait ElvenAgility = new RacialTrait();
            RacialTrait Ferocious = new RacialTrait();
            RacialTrait Hardy = new RacialTrait();
            RacialTrait Ironhide = new RacialTrait();
            RacialTrait IronWillpower = new RacialTrait();
            RacialTrait KeenSenses1 = new RacialTrait();
            RacialTrait KeenSenses2 = new RacialTrait();
            RacialTrait NaturalAthlete = new RacialTrait();
            RacialTrait NightSight1 = new RacialTrait();
            RacialTrait NightSight2 = new RacialTrait();
            RacialTrait Primal = new RacialTrait();
            RacialTrait Resourceful = new RacialTrait();
            RacialTrait Savagery = new RacialTrait();
            RacialTrait TechnicalAptitude1 = new RacialTrait();
            RacialTrait TechnicalAptitude2 = new RacialTrait();
            RacialTrait ViperWoodHeritage = new RacialTrait();

            Adaptable.Name = "Adaptable";
            Adaptable.Description = "The cost of each purchased Skill Value is reduced by 1.";
            Adaptable.Race = "Human";
            Adaptable.RTPCost = 3;

            #region Affinity

            Affinity1.Name = "Affinity (Spirit)";
            Affinity1.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity1.Race = "Human";
            Affinity1.RTPCost = 1;

            Affinity2.Name = "Affinity (Stamina Regen)";
            Affinity2.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity2.Race = "Human";
            Affinity2.RTPCost = 1;

            Affinity3.Name = "Affinity (Alteration)";
            Affinity3.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity3.Race = "Dwarf";
            Affinity3.RTPCost = 2;

            Affinity4.Name = "Affinity (Construction + Mechanics + Smithing)";
            Affinity4.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity4.Race = "Dwarf";
            Affinity4.RTPCost = 2;

            Affinity5.Name = "Affinity (Dueling)";
            Affinity5.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity5.Race = "Dwarf";
            Affinity5.RTPCost = 2;

            Affinity6.Name = "Affinity (Enchantment)";
            Affinity6.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity6.Race = "Dwarf";
            Affinity6.RTPCost = 2;

            Affinity7.Name = "Affinity (Fortitude)";
            Affinity7.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity7.Race = "Dwarf";
            Affinity7.RTPCost = 2;

            Affinity8.Name = "Affinity (Invocation)";
            Affinity8.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity8.Race = "Dwarf";
            Affinity8.RTPCost = 2;

            Affinity9.Name = "Affinity (Bows)";
            Affinity9.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity9.Race = "Elf";
            Affinity9.RTPCost = 2;

            Affinity10.Name = "Affinity (Artistry + Healing + Survival)";
            Affinity10.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity10.Race = "Elf";
            Affinity10.RTPCost = 2;

            Affinity11.Name = "Affinity (Athletics + Stealth + Survival)";
            Affinity11.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity11.Race = "Elf";
            Affinity11.RTPCost = 2;

            Affinity12.Name = "Affinity (Conjuration)";
            Affinity12.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity12.Race = "Elf";
            Affinity12.RTPCost = 2;

            Affinity13.Name = "Affinity (Deception + Persuasion + Negotiation)";
            Affinity13.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity13.Race = "Elf";
            Affinity13.RTPCost = 2;

            Affinity14.Name = "Affinity (Dueling)";
            Affinity14.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity14.Race = "Elf";
            Affinity14.RTPCost = 2;

            Affinity15.Name = "Affinity (Presence)";
            Affinity15.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity15.Race = "Elf";
            Affinity15.RTPCost = 2;

            Affinity16.Name = "Affinity (Any 2 non-combat skills)";
            Affinity16.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity16.Race = "Human";
            Affinity16.RTPCost = 2;

            Affinity17.Name = "Affinity (Close Combat)";
            Affinity17.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity17.Race = "Human";
            Affinity17.RTPCost = 2;

            Affinity18.Name = "Affinity (Electronics + Weapon Systems + )";
            Affinity18.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity18.Race = "Human";
            Affinity18.RTPCost = 2;

            Affinity19.Name = "Affinity (Longarms)";
            Affinity19.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity19.Race = "Human";
            Affinity19.RTPCost = 2;

            Affinity20.Name = "Affinity (Presence)";
            Affinity20.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity20.Race = "Human";
            Affinity20.RTPCost = 2;

            Affinity21.Name = "Affinity (Shortarms)";
            Affinity21.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity21.Race = "Human";
            Affinity21.RTPCost = 2;

            Affinity22.Name = "Affinity (Smithing + Mechanics + Construction)";
            Affinity22.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity22.Race = "Human";
            Affinity22.RTPCost = 2;

            Affinity23.Name = "Affinity (Athletics + Survival + Smithing)";
            Affinity23.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity23.Race = "Orc";
            Affinity23.RTPCost = 2;

            Affinity24.Name = "Affinity (Fortitude)";
            Affinity24.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity24.Race = "Orc";
            Affinity24.RTPCost = 2;

            Affinity25.Name = "Affinity (Heavy)";
            Affinity25.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity25.Race = "Orc";
            Affinity25.RTPCost = 2;

            Affinity26.Name = "Affinity (Invocation)";
            Affinity26.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity26.Race = "Orc";
            Affinity26.RTPCost = 2;

            Affinity27.Name = "Affinity (Thrown)";
            Affinity27.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity27.Race = "Orc";
            Affinity27.RTPCost = 2;

            Affinity28.Name = "Affinity (Unarmed)";
            Affinity28.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity28.Race = "Orc";
            Affinity28.RTPCost = 2;

            Affinity29.Name = "Affinity (Strength)";
            Affinity29.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity29.Race = "Dwarf";
            Affinity29.RTPCost = 3;

            Affinity30.Name = "Affinity (Willpower)";
            Affinity30.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity30.Race = "Dwarf";
            Affinity30.RTPCost = 3;

            Affinity31.Name = "Affinity (Agility)";
            Affinity31.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity31.Race = "Elf";
            Affinity31.RTPCost = 3;

            Affinity32.Name = "Affinity (Willpower)";
            Affinity32.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity32.Race = "Elf";
            Affinity32.RTPCost = 3;

            Affinity33.Name = "Affinity (Focus)";
            Affinity33.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity33.Race = "Human";
            Affinity33.RTPCost = 3;

            Affinity34.Name = "Affinity (Strength)";
            Affinity34.Description = "Gain a +1 bonus to the affected Attribute or Skill. The rank of the Attribute or Skill does not change (and thus neither does the cost to increase it), only the effective rank.";
            Affinity34.Race = "Orc";
            Affinity34.RTPCost = 3;

            #endregion

            Animalistic.Name = "Animalistic";
            Animalistic.Description = "Gain +2 CM with Unarmed attacks. This Trait coincides with the presence of horns and / or pronounced claws or teeth as a character aesthetic.";
            Animalistic.Race = "Orc";
            Animalistic.RTPCost = 2;

            ArcaneHeritage.Name = "ArcaneHeritage";
            ArcaneHeritage.Description = "Gain 10 Build Points (BP) toward a Spellcasting Talent Tree.";
            ArcaneHeritage.Race = "Elf";
            ArcaneHeritage.RTPCost = 1;

            ArcaneElite.Name = "ArcaneElite";
            ArcaneElite.Description = "Gain 25 BP to use among Spellcasting Talent Trees.";
            ArcaneElite.Race = "Elf";
            ArcaneElite.RTPCost = 3;

            BeastWithin.Name = "BeastWithin";
            BeastWithin.Description = "Gain +1 to Speed, +2 to Unarmed damage, and 8 BP among the Brawling and / or Quickness Talent Trees.";
            BeastWithin.Race = "Orc";
            BeastWithin.RTPCost = 3;

            Connected.Name = "Connected";
            Connected.Description = "You get a 20% reduced price for 1 type of item(see Contacts in Backgrounds) and a + 2 to Connection Checks to find the selected items. This Trait stacks with the Contacts benefit from Backgrounds.";
            Connected.Race = "Dwarf";
            Connected.RTPCost = 1;

            Earthborn.Name = "Earthborn";
            Earthborn.Description = "Gain 10 BP to use among Earthshaper and/ or Toughness Talent Trees.";
            Earthborn.Race = "Dwarf";
            Earthborn.RTPCost = 1;

            EarthbornNoble.Name = "EarthbornNoble";
            EarthbornNoble.Description = "Gain 25 BP to use among Earthshaping and/ or Toughness Talent Trees.";
            EarthbornNoble.Race = "Dwarf";
            EarthbornNoble.RTPCost = 3;

            Educated.Name = "Educated";
            Educated.Description = "Gain 15 BP to use among Knowledge Skills.";
            Educated.Race = "Human";
            Educated.RTPCost = 1;

            ElvenAgility.Name = "ElvenAgility";
            ElvenAgility.Description = "Gain +1 Speed and 8 BP toward the Quickness Talent Tree.";
            ElvenAgility.Race = "Elf";
            ElvenAgility.RTPCost = 1;

            Ferocious.Name = "Ferocious";
            Ferocious.Description = "Gain Lethal 1 with Unarmed attacks.";
            Ferocious.Race = "Orc";
            Ferocious.RTPCost = 2;

            Hardy.Name = "Hardy";
            Hardy.Description = "Gain +1 Health Points (HP) per Damage Track.";
            Hardy.Race = "Dwarf";
            Hardy.RTPCost = 1;

            Ironhide.Name = "Ironhide";
            Ironhide.Description = "Gain +1 Durability and +1 Body Defense.";
            Ironhide.Race = "Dwarf";
            Ironhide.RTPCost = 2;

            IronWillpower.Name = "IronWillpower";
            IronWillpower.Description = "Gain +1 to Resolve Defenses and Resistance Checks.";
            IronWillpower.Race = "Orc";
            IronWillpower.RTPCost = 1;

            KeenSenses1.Name = "KeenSenses";
            KeenSenses1.Description = "Gain +2 to Perception.";
            KeenSenses1.Race = "Elf";
            KeenSenses1.RTPCost = 1;

            KeenSenses2.Name = "KeenSenses";
            KeenSenses2.Description = "Gain +2 to Perception.";
            KeenSenses2.Race = "Orc";
            KeenSenses2.RTPCost = 1;

            NaturalAthlete.Name = "NaturalAthlete";
            NaturalAthlete.Description = "Gain 11 BP toward the Quickness Talent Tree.";
            NaturalAthlete.Race = "Orc";
            NaturalAthlete.RTPCost = 1;

            NightSight1.Name = "NightSight";
            NightSight1.Description = "Darkness does not impose Concealment penalties against you or provide Concealment when establishing or maintaining Stealth against you.";
            NightSight1.Race = "Dwarf";
            NightSight1.RTPCost = 1;

            NightSight2.Name = "NightSight";
            NightSight2.Description = "Darkness does not impose Concealment penalties against you or provide Concealment when establishing or maintaining Stealth against you.";
            NightSight2.Race = "Elf";
            NightSight2.RTPCost = 1;

            Primal.Name = "Primal";
            Primal.Description = "Do not suffer wound penalties to damage, and gain +2 to damage while suffering wound penalties to attack.";
            Primal.Race = "Orc";
            Primal.RTPCost = 3;

            Resourceful.Name = "Resourceful";
            Resourceful.Description = "The cost of all Talents is reduced by 1.";
            Resourceful.Race = "Human";
            Resourceful.RTPCost = 3;

            Savagery.Name = "Savagery";
            Savagery.Description = "Gain +1 CM with Melee attacks.";
            Savagery.Race = "Orc";
            Savagery.RTPCost = 2;

            TechnicalAptitude1.Name = "TechnicalAptitude";
            TechnicalAptitude1.Description = "Gain 8 BP toward the Science Talent Tree and pay - 10 % on the total cost of all equipment you craft.";
            TechnicalAptitude1.Race = "Dwarf";
            TechnicalAptitude1.RTPCost = 1;

            TechnicalAptitude2.Name = "TechnicalAptitude";
            TechnicalAptitude2.Description = "Gain 8 BP toward the Science Talent Tree and pay - 10 % on the total cost of all equipment you craft.";
            TechnicalAptitude2.Race = "Human";
            TechnicalAptitude2.RTPCost = 1;

            ViperWoodHeritage.Name = "ViperWoodHeritage";
            ViperWoodHeritage.Description = "Gain +4 Durability vs. Poison damage and Immunity to the Poison environment of the Viper Wood.";
            ViperWoodHeritage.Race = "Elf";
            ViperWoodHeritage.RTPCost = 1;

            availableRacialTraits = new ObservableCollection<RacialTrait>();
            availableRacialTraits.Add(Adaptable);
            availableRacialTraits.Add(Affinity1);
            availableRacialTraits.Add(Affinity2);
            availableRacialTraits.Add(Affinity3);
            availableRacialTraits.Add(Affinity4);
            availableRacialTraits.Add(Affinity5);
            availableRacialTraits.Add(Affinity6);
            availableRacialTraits.Add(Affinity7);
            availableRacialTraits.Add(Affinity8);
            availableRacialTraits.Add(Affinity9);
            availableRacialTraits.Add(Affinity10);
            availableRacialTraits.Add(Affinity11);
            availableRacialTraits.Add(Affinity12);
            availableRacialTraits.Add(Affinity13);
            availableRacialTraits.Add(Affinity14);
            availableRacialTraits.Add(Affinity15);
            availableRacialTraits.Add(Affinity16);
            availableRacialTraits.Add(Affinity17);
            availableRacialTraits.Add(Affinity18);
            availableRacialTraits.Add(Affinity19);
            availableRacialTraits.Add(Affinity20);
            availableRacialTraits.Add(Affinity21);
            availableRacialTraits.Add(Affinity22);
            availableRacialTraits.Add(Affinity23);
            availableRacialTraits.Add(Affinity24);
            availableRacialTraits.Add(Affinity25);
            availableRacialTraits.Add(Affinity26);
            availableRacialTraits.Add(Affinity27);
            availableRacialTraits.Add(Affinity28);
            availableRacialTraits.Add(Affinity29);
            availableRacialTraits.Add(Affinity30);
            availableRacialTraits.Add(Affinity31);
            availableRacialTraits.Add(Affinity32);
            availableRacialTraits.Add(Affinity33);
            availableRacialTraits.Add(Affinity34);
            availableRacialTraits.Add(Animalistic);
            availableRacialTraits.Add(ArcaneHeritage);
            availableRacialTraits.Add(ArcaneElite);
            availableRacialTraits.Add(BeastWithin);
            availableRacialTraits.Add(Connected);
            availableRacialTraits.Add(Earthborn);
            availableRacialTraits.Add(EarthbornNoble);
            availableRacialTraits.Add(Educated);
            availableRacialTraits.Add(ElvenAgility);
            availableRacialTraits.Add(Ferocious);
            availableRacialTraits.Add(Hardy);
            availableRacialTraits.Add(Ironhide);
            availableRacialTraits.Add(IronWillpower);
            availableRacialTraits.Add(KeenSenses1);
            availableRacialTraits.Add(KeenSenses2);
            availableRacialTraits.Add(NaturalAthlete);
            availableRacialTraits.Add(NightSight1);
            availableRacialTraits.Add(NightSight2);
            availableRacialTraits.Add(Primal);
            availableRacialTraits.Add(Resourceful);
            availableRacialTraits.Add(Savagery);
            availableRacialTraits.Add(TechnicalAptitude1);
            availableRacialTraits.Add(TechnicalAptitude2);
            availableRacialTraits.Add(ViperWoodHeritage);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
