using API.Mapper.Attributes;
using DAL.Entities.Entities;
using DAL.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
   
    public static class MapperSpecifiques
    {
        #region Map BossZone E to M
        public static BossesZoneModel ToModel(this BossesZoneEntity BZE)
        {
            if (BZE != null)
            {
                BossRepository repoBoss = new BossRepository();
                ZoneRepository repoZone = new ZoneRepository();
                BossesZoneModel BZM = new BossesZoneModel();
                BZM.Boss = repoBoss.GetOne(BZE.BossId).MapTo<BossModel>();
                BZM.Zone = repoZone.GetOne(BZE.ZoneId).MapTo<ZoneModel>();
                BZM.Active = BZE.Active;
                BZM.Id = BZE.Id;
                return BZM;
            }
            else return null;
        }
        #endregion

        #region Map BossZone M to E
        public static BossesZoneEntity ToEntity(this BossesZoneModel BZM)
        {
            if (BZM != null)
            {
                BossesZoneEntity BZE = new BossesZoneEntity();
                BZE.BossId = BZM.Zone.Id;
                BZE.ZoneId = BZM.Boss.Id;
                BZE.Id = BZM.Id;
                BZE.Active = BZM.Active;
                return BZE;
            }
            else return null;
        }
        #endregion

        #region Map Strategy E to M
        public static StrategyModel ToModel(this StrategyEntity SE)
        {
            if (SE != null)
            {
                BossesPerZoneRepository repoBZ = new BossesPerZoneRepository();
                CharactersConfigurationRepository repoCC = new CharactersConfigurationRepository();
                UserRepository repoU = new UserRepository();
                StrategyModel SM = new StrategyModel();
                SM.User = repoU.GetOne(SE.UserId).MapTo<UserModel>();
                SM.CharactersConfiguration = repoCC.GetOne(SE.CharactersConfigurationId).ToModel();
                SM.BossZone = repoBZ.GetOne(SE.BossZoneId).ToModel();
                SM.ImagePath1 = SE.ImagePath1;
                SM.ImagePath2 = SE.ImagePath2;
                SM.ImagePath3 = SE.ImagePath3;
                SM.ImagePath4 = SE.ImagePath4;
                SM.Description = SE.Description;
                SM.Note = SE.Note;
                SM.Id = SE.Id;
                SM.Active = SE.Active;
                return SM;
            }
            else return null;
        }
        #endregion

        #region Map Strategy M to E
        public static StrategyEntity ToEntity(this StrategyModel SM)
        {
            if (SM != null)
            {
                StrategyEntity SE = new StrategyEntity();
                SE.UserId = SM.User.Id;
                SE.CharactersConfigurationId = SM.CharactersConfiguration.Id;
                SE.BossZoneId = SM.BossZone.Id;
                SE.ImagePath1 = SM.ImagePath1;
                SE.ImagePath2 = SM.ImagePath2;
                SE.ImagePath3 = SM.ImagePath3;
                SE.ImagePath4 = SM.ImagePath4;
                SE.Description = SM.Description;
                SE.Note = SM.Note;
                SE.Id = SM.Id;
                SE.Active = SM.Active;
                return SE;
            }
            else return null;
        }
        #endregion

        #region Map FavoriteStrategy E to M
        public static FavoriteStrategyModel ToModel(this FavoriteStrategyEntity FE)
        {
            if (FE != null)
            {
                StrategyRepository repoEnre = new StrategyRepository();
                UserRepository repoUtil = new UserRepository();
                FavoriteStrategyModel FM = new FavoriteStrategyModel();
                FM.Strategy = repoEnre.GetOne(FE.StrategyId).ToModel();
                FM.User = repoUtil.GetOne(FE.UserId).MapTo<UserModel>();
                FM.Id = FE.Id;
                FM.Active = FE.Active;
                return FM;
            }
            else return null;
        }
        #endregion

        #region Map FavoriteStrategy M to E
        public static FavoriteStrategyEntity ToEntity(this FavoriteStrategyModel FM)
        {
            if (FM != null)
            {
                FavoriteStrategyEntity FE = new FavoriteStrategyEntity();
                FE.StrategyId = FM.Strategy.Id;
                FE.UserId = FM.User.Id;
                FE.Id = FM.Id;
                FE.Active = FM.Active;
                return FE;
            }
            else return null;
        }
        #endregion

        #region Map Vote E to M
        public static VoteModel ToModel(this VoteEntity VE)
        {
            if (VE != null)
            {
                StrategyRepository repoStr = new StrategyRepository();
                UserRepository repoUtil = new UserRepository();
                VoteModel VM = new VoteModel();
                VM.Strategy = repoStr.GetOne(VE.StrategyId).ToModel();
                VM.Id = VE.Id;
                VM.User = repoUtil.GetOne(VE.UserId).MapTo<UserModel>();
                VM.Vote = VE.Vote;
                VM.Active = VE.Active;
                return VM;
            }
            else return null;
        }
        #endregion

        #region Map Vote M to E
        public static VoteEntity ToEntity(this VoteModel VM)
        {
            if (VM != null)
            {
                VoteEntity VE = new VoteEntity();
                VE.StrategyId = VM.Strategy.Id;
                VE.UserId = VM.User.Id;
                VE.Id = VM.Id;
                VE.Vote = VM.Vote;
                VE.Active = VM.Active;
                return VE;
            }
            else return null;
        }
        #endregion

        #region Map Skill E to M
        public static SkillModel ToModel(this SkillEntity SE)
        {
            if (SE != null)
            {
                ClasseRepository repoCl = new ClasseRepository();
                SkillModel SM = new SkillModel();
                SM.Id = SE.Id;
                SM.NameEN = SE.NameEN;
                SM.NameFR = SE.NameFR;
                SM.DescriptionEN = SE.DescriptionEN;
                SM.DescriptionFR = SE.DescriptionFR;
                SM.Location = SE.Location;
                SM.Cost = SE.Cost;
                SM.ImagePath = SE.ImagePath;
                SM.Classe = repoCl.GetOne(SE.ClasseId).MapTo<ClasseModel>();
                SM.Active = SE.Active;
                return SM;
            }
            else return null;
        }
        #endregion

        #region Map Skill M to E
        public static SkillEntity ToEntity(this SkillModel SM)
        {
            if (SM != null)
            {
                SkillEntity SE = new SkillEntity();
                SE.Id = SM.Id;
                SE.NameEN = SM.NameEN;
                SE.NameFR = SM.NameFR;
                SE.DescriptionEN = SM.DescriptionEN;
                SE.DescriptionFR = SM.DescriptionFR;
                SE.Location = SM.Location;
                SE.Cost = SM.Cost;
                SE.ImagePath = SM.ImagePath;
                SE.ClasseId = SM.Classe.Id;
                SE.Active = SM.Active;
                return SE;
            }
            else return null;
        }
        #endregion

        #region Map CharactersConfiguration E to M
        public static CharactersConfigurationModel ToModel(this CharactersConfigurationEntity CCE)
        {
            if (CCE != null)
            {
                ClasseRepository repoCl = new ClasseRepository();
                CharactersConfigurationModel CCM = new CharactersConfigurationModel();
                CCM.Id = CCE.Id;
                CCM.Classe1 = repoCl.GetOne(CCE.ClasseId1).MapTo<ClasseModel>();
                CCM.Classe2 = repoCl.GetOne(CCE.ClasseId2).MapTo<ClasseModel>();
                CCM.Classe3 = repoCl.GetOne(CCE.ClasseId3).MapTo<ClasseModel>();
                CCM.Classe4 = repoCl.GetOne(CCE.ClasseId4).MapTo<ClasseModel>();
                CCM.Active = CCE.Active;
                return CCM;
            }
            else return null;
        }
        #endregion

        #region Map CharactersConfiguration M to E
        public static CharactersConfigurationEntity ToEntity(this CharactersConfigurationModel CCM)
        {
            if (CCM != null)
            {
                CharactersConfigurationEntity CCE = new CharactersConfigurationEntity();
                CCE.Id = CCM.Id;
                CCE.ClasseId1 = CCM.Classe1.Id;
                CCE.ClasseId2 = CCM.Classe2.Id;
                CCE.ClasseId3 = CCM.Classe3.Id;
                CCE.ClasseId4 = CCM.Classe4.Id;
                CCE.Active = CCM.Active;
                return CCE;
            }
            else return null;
        }
        #endregion

        #region Map Team E to M
        public static TeamModel ToModel(this TeamEntity TE)
        {
            if (TE != null)
            {
                ZoneRepository repoZ = new ZoneRepository();
                UserRepository repoU = new UserRepository();
                CharactersConfigurationRepository repoCC = new CharactersConfigurationRepository();
                TeamModel TM = new TeamModel();
                TM.Id = TE.Id;
                TM.CharactersConfiguration = repoCC.GetOne(TE.CharactersConfigurationId).ToModel();
                TM.TeamName = TE.TeamName;
                TM.Zone = repoZ.GetOne(TE.ZoneId).MapTo<ZoneModel>();
                TM.User = repoU.GetOne(TE.UserId).MapTo<UserModel>();
                TM.Active = TE.Active;
                return TM;
            }
            else return null;
        }
        #endregion

        #region Map Team M to E
        public static TeamEntity ToEntity(this TeamModel TM)
        {
            if (TM != null)
            {
                TeamEntity TE = new TeamEntity();
                TE.Id = TM.Id;
                TE.CharactersConfigurationId = TM.CharactersConfiguration.Id;
                TE.TeamName = TM.TeamName;
                TE.ZoneId = TM.Zone.Id;
                TE.UserId = TM.User.Id;
                TE.Active = TM.Active;
                return TE;
            }
            else return null;
        }
        #endregion

        #region Map Follow E to M
        public static FollowModel ToModel(this FollowEntity FE)
        {
            if (FE != null)
            {
                UserRepository repoU = new UserRepository();
                FollowModel FM = new FollowModel();
                FM.Followed = repoU.GetOne(FE.FollowedId).MapTo<UserModel>();
                FM.Follower = repoU.GetOne(FE.FollowerId).MapTo<UserModel>();
                return FM;
            }
            else return null;
        }
        #endregion

        #region Map Follow M to E
        public static FollowEntity ToEntity(this FollowModel FM)
        {
            if (FM != null)
            {
                FollowEntity FE = new FollowEntity();
                FE.FollowedId = FM.Followed.Id;
                FE.FollowerId = FM.Follower.Id;

                return FE;
            }
            else return null;
        }
        #endregion
    }
}
