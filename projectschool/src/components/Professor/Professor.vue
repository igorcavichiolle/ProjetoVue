<template>
  <div>
    <Titulo texto="Professores" btnVoltar="true"/>
    <table>
      <thead>
        <th>Cód</th>
        <th>Nome</th>
        <th>Alunos</th>
      </thead>
      <tbody v-if="Professores.length">
        <tr v-for="(professor, index) in Professores" :key="index">
          <td class="colPequeno">{{ professor.id }}</td>
          <!-- <td>{{aluno.id}}</td> -->

          <router-link
            :to="`/alunos/${professor.id}`"
            tag="td"
            style="cursor: pointer"
          >
            {{ professor.nome }} {{ professor.sobrenome }}
          </router-link>

          <td class="colPequeno">
            {{ professor.qtdAlunos }}
          </td>
        </tr>
      </tbody>
      <tfoot v-else>
        <tr>
          <td colspan="3" style="text-align: center"> 
            <h5>Nenhum professor encontrado</h5>
            
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>

<script>
import Titulo from "../_share/Titulo";
export default {
  components: {
    Titulo,
  },
  data() {
    return {
      Professores: [],
      Alunos: [],
    };
  },
  created() {
    //carrega todos os alunos
    this.$http
      .get("http://localhost:5000/api/aluno")
      .then((res) => res.json())
      .then((alunos) => {
        this.Alunos = alunos;
        //carrega os professores
        this.carregarProfessores();
      });
  },
  props: {},
  methods: {
    pegarQtdAlunosPorProfessor() {
      //foreach para cada professor
      this.Professores.forEach((professor, index) => {
        professor = {
          id: professor.id,
          nome: professor.nome,
          //soma da quantidade de alunos por professor
          qtdAlunos: this.Alunos.filter(
            (aluno) => aluno.professor.id == professor.id
          ).length,
        };
        this.Professores[index] = professor;
      });
    },
    //carrega todos os professores
    carregarProfessores() {
      this.$http
        .get("http://localhost:5000/api/professor")
        .then((res) => res.json())
        .then((professor) => {
          this.Professores = professor;
          //pega quantos alunos cada professor tem
          this.pegarQtdAlunosPorProfessor();
        });
    },
  },
};
</script>

<style scoped>
.colPequeno {
  text-align: center;
  width: 15%;
}
</style>
